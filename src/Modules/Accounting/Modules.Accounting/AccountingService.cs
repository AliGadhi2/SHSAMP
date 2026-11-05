using FSH.Framework.Core.Exceptions;
using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.Dtos;
 
using FSH.Modules.Accounting.Domain;
using FSH.Modules.Accounting.Data;
using FSH.Modules.Accounting.Features.v1.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ImemoryCache = Microsoft.Extensions.Caching.Memory.IMemoryCache;
using Microsoft.Extensions.Logging;
namespace FSH.Modules.Accounting.Services;


public sealed class AccountingService : IAccountingService
{
    private readonly AccountingDbContext _db;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AccountingService> _logger;

    public AccountingService(
        AccountingDbContext db,
        IMemoryCache cache,
        ILogger<AccountingService> logger)
    {
        _db = db;
        _cache = cache;
        _logger = logger;
    }
 

    public async Task<AccountDto> UpdateAccountinfo( Guid accountid,
       
        string accountName,
        long accountNumber,
        Guid accountFatherID,
        AccountType accountType,
        string notes,
        AccountReference accountRefrense, CancellationToken cancellationToken = default)
    {
        var account = await _cache.GetOrCreateAsync($"account_{accountid}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(50); // Cache for 50 minutes
            entry.SetPriority(CacheItemPriority.High); // Set high priority to avoid eviction
            entry.SlidingExpiration = TimeSpan.FromMinutes(5); // Reset expiration if accessed within 1 minute
           entry.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                // Handle cache eviction if needed
                // For example, log the eviction reason
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Cache entry evicted. Key: {CacheKey}, Reason: {Reason}.", key, reason);
                }
            });
            return await LoadAccountAsync(accountid, cancellationToken).ConfigureAwait(false);
        }) ?? throw new UnauthorizedException("Tenant context is required.");
        
       
        account.UpdateAccountinfo(accountName,
        accountNumber,
        accountFatherID,
        accountType,
        notes,
        accountRefrense);
        return account.ToDto();

    }

    public async Task UpdateAccountAsync(Guid accountId, string accountName, Guid accountFatherID, AccountType accountType,
        AccountReference accountRefrense, string? notes, CancellationToken cancellationToken = default)
    {
        var account = await LoadAccountAsync(accountId, cancellationToken).ConfigureAwait(false);
        var children = await _db.Accounts
            .Where(item => item.AccountFatherId == account.Id && item.Id != accountId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
            var FatherAccountNumber = await _db.Accounts.FirstOrDefaultAsync(item => item.Id == accountFatherID, cancellationToken).ConfigureAwait(false)?? throw new CustomException("The selected parent account was not found.",
                Enumerable.Empty<string>(), System.Net.HttpStatusCode.BadRequest);
        if (children.Count > 0 && account.AccountType != accountType)
        {
            throw new CustomException("The account type cannot change while the account has children.",
                Enumerable.Empty<string>(), System.Net.HttpStatusCode.BadRequest);
        }

        if (children.Count > 0 && accountFatherID != account.AccountFatherId)
        {
            throw new CustomException("An account with children cannot be moved. Move its child accounts first.",
                Enumerable.Empty<string>(), System.Net.HttpStatusCode.BadRequest);
        }

        if (accountFatherID == account.Id)
        {
            throw new CustomException("An account cannot be its own parent.",
                Enumerable.Empty<string>(), System.Net.HttpStatusCode.BadRequest);
        }

        if (accountFatherID != Guid.Empty)
        {
            var parent = await _db.Accounts.SingleOrDefaultAsync(
                item => item.Id == accountFatherID, cancellationToken).ConfigureAwait(false);
            if (parent is null || parent.AccountType != AccountType.Sub)
            {
                throw new CustomException("The selected parent must be a main account.",
                    Enumerable.Empty<string>(), System.Net.HttpStatusCode.BadRequest);
            }
        }

        var siblings = await _db.Accounts
            .Where(item => item.AccountFatherId == accountFatherID && item.Id != accountId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        if (siblings.Count > 0 && siblings.Any(item => item.AccountType != accountType))
        {
            throw new CustomException("Child accounts must use the same account type.",
                Enumerable.Empty<string>(), System.Net.HttpStatusCode.BadRequest);
        }

        long newAccountNumber = FatherAccountNumber.AccountNumber == 0
            ? (siblings.Select(item => (long?)item.AccountNumber).Max() ?? 0) + 1
            : Enumerable.Range(1, 9)
                .Select(index => checked(FatherAccountNumber.AccountNumber * 10 + index))
                .FirstOrDefault(candidate => siblings.All(item => item.AccountNumber != candidate));
        if (newAccountNumber == 0)
        {
            throw new CustomException("The parent account has reached its maximum child accounts.",
                Enumerable.Empty<string>(), System.Net.HttpStatusCode.BadRequest);
        }

        bool duplicateName = await _db.Accounts.AnyAsync(
            item => item.Id != accountId && item.AccountName == accountName,
            cancellationToken).ConfigureAwait(false);
        if (duplicateName)
        {
            throw new CustomException("An account with this name already exists for the tenant.",
                Enumerable.Empty<string>(), System.Net.HttpStatusCode.Conflict);
        }

        account.UpdateAccountinfo(accountName, newAccountNumber, accountFatherID, accountType, notes ?? "", accountRefrense);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
   

    // Issue/MarkPaid/Void load here. BillingDbContext isn't tenant-filtered, so scope to caller: root
    // mutates any invoice; a tenant caller is pinned to its own (cross-tenant id → 404, can't mutate).
    private async Task<Account> LoadAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await _db.Accounts
            .FirstOrDefaultAsync(i => i.Id == accountId, cancellationToken)
            .ConfigureAwait(false)
            ?? throw new NotFoundException($"account {accountId} not found.");
    }


    
     





    public async Task IssueAccountAsync(Guid accountId, DateTime? dueAtUtc, CancellationToken cancellationToken = default)
    {
        var account = await LoadAccountAsync(accountId, cancellationToken).ConfigureAwait(false);
        account.Issue(dueAtUtc);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task MarkAccountStopAsync(Guid accountId, string? reson, CancellationToken cancellationToken = default)
    {
        var account = await LoadAccountAsync(accountId, cancellationToken).ConfigureAwait(false);
        account.MarkStop();
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task MarkAccountActiveAsync(Guid accountId, string? reason, CancellationToken cancellationToken = default)
    {
        var account = await LoadAccountAsync(accountId, cancellationToken).ConfigureAwait(false);
        account.MarkActive();
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await LoadAccountAsync(accountId, cancellationToken).ConfigureAwait(false);
        bool hasChildren = await _db.Accounts.AnyAsync(
            child => child.AccountFatherId == account.Id,
            cancellationToken).ConfigureAwait(false);
        if (hasChildren)
        {
            throw new CustomException(
                "An account with child accounts cannot be deleted.",
                Enumerable.Empty<string>(),
                System.Net.HttpStatusCode.Conflict);
        }

        _db.Accounts.Remove(account);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DepandingAccountDraft(Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await LoadAccountAsync(accountId, cancellationToken).ConfigureAwait(false);
        account.Depanding();
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

 
}

    