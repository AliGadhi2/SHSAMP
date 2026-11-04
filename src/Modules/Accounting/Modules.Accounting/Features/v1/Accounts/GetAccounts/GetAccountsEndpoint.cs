using FSH.Framework.Shared.Identity.Authorization;
using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.Authorization;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.Accounting.Features.v1.Accounts.GetAccounts;

public static class GetAccountsEndpoint
{
    internal static RouteHandlerBuilder MapGetAccountsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/accounts",
                (string? tenantId, AccountStatus? status, long? accountFather, string? accountrefrense,
                 int pageNumber, int pageSize, IMediator mediator, CancellationToken ct) =>
                    mediator.Send(new GetAccountsQuery(
                        tenantId,
                        status,
                         accountFather,
                        accountrefrense,
                        pageNumber <= 0 ? 1 : pageNumber,
                        pageSize <= 0 ? 20 : Math.Min(pageSize, 100)), ct))
            .WithName("GetAccounts")
            .WithSummary("List Accounts  (admin)")
            .RequirePermission(AccountingPermissions.Accounts.View);
    }
}
