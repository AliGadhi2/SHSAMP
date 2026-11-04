using FSH.Framework.Shared.Persistence;
using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Features.v1.Accounts.GetAccounts;

public sealed class GetAccountsQueryHandler(
    AccountingDbContext dbContext)
    : IQueryHandler<GetAccountsQuery, PagedResponse<AccountDto>>
{
    public async ValueTask<PagedResponse<AccountDto>> Handle(GetAccountsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        // BillingDbContext is not tenant-filtered: only root gets the cross-tenant view (optionally
        // narrowed via query.TenantId); every other caller is forced to its own tenant.
        var q = dbContext.Accounts.AsNoTracking().AsQueryable();
        if (query.Status is not null)
        {
            q = q.Where(i => i.Status == query.Status);
        }
        if (query.accountrefrense is not null)
        {
            q = q.Where(i => i.AccountRefrense == query.accountrefrense);
        }
        if (query.accountFather is not null)
        {
            q = q.Where(i => i.AccountFather == query.accountFather);
        }

        var total = await q.LongCountAsync(cancellationToken).ConfigureAwait(false);
        var accounts = await q
            .OrderByDescending(i => i.CreatedAtUtc)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return new PagedResponse<AccountDto>
        {
            Items = accounts.Select(i => i.ToDto()).ToList(),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)query.PageSize)
        };
    }
}
