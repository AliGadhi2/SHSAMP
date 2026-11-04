using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Modules.Accounting.Domain;

namespace FSH.Modules.Accounting.Services;
/// <summary>
/// Core billing workflow: snapshot usage, price it, issue the invoice, track payment state.
/// Payment processor integration is intentionally out of scope — invoices are marked paid manually.
/// </summary>
public interface IAccountingService
{
    /// <summary>
    /// Generates a Draft invoice for the tenant/period by snapshotting usage and pricing it against
    /// the tenant's active subscription plan. Returns null if the tenant has no active subscription
    /// or an invoice already exists for the period.
    /// </summary>
    Task<AccountDto> GenerateAccountAsync(
        string accountName,
        long accountfather,
        int accounttype,
        string note,
        string accountRefrense,
        CancellationToken cancellationToken = default);
    Task<AccountDto> UpdateAccountinfo(Guid accountid,

     string accountName,
     long accountNumber,
     long accountFather,
     int accountype,
     string notes,
     string accountRefrense, CancellationToken cancellationToken = default);
    Task UpdateAccountAsync(Guid accountId, string accountName, long accountFather, int accountType,
        string accountRefrense, string? notes, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates and issues a Subscription-purpose invoice for one plan term (the term base fee). Called
    /// when a tenant subscribes or renews. Returns null for free/zero-price plans (no invoice).
    /// Idempotent: returns the existing invoice if one already exists for the term.
    /// </summary>


    Task IssueAccountAsync(Guid accountId, DateTime? dueAtUtc, CancellationToken cancellationToken = default);

    Task MarkAccountStopAsync(Guid accountId, string? reson, CancellationToken cancellationToken = default);
    Task DepandingAccountDraft(Guid accountId, CancellationToken cancellationToken = default);
    Task MarkAccountActiveAsync(Guid accountId, string? reason, CancellationToken cancellationToken = default);
    Task DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default);

}