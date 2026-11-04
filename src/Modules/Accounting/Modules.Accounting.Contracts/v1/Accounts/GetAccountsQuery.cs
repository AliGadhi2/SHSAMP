using FSH.Framework.Shared.Persistence;
using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
/// <summary>
/// Admin query — lists invoices across all tenants with optional filters. Tenant-scoped callers
/// should use <c>GetMyInvoicesQuery</c> instead to avoid leaking cross-tenant data.
/// </summary>
public sealed record GetAccountsQuery(
    string? TenantId = null,
    AccountStatus? Status = null,
    long? accountFather = null,
    string? accountrefrense =null ,
    int PageNumber = 1,
    int PageSize = 20) : IQuery<PagedResponse<AccountDto>>;
