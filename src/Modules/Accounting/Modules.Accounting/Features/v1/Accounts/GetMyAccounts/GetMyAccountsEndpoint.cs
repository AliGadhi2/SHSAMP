using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.Accounting.Features.v1.Accounts.GetMyAccounts;

public static class GetMyAccountsEndpoint
{
    internal static RouteHandlerBuilder MapGetMyAccountsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/accounts/me",
                (AccountStatus? status, long? accountFather, int? accounttype,
                 int pageNumber, int pageSize, IMediator mediator, CancellationToken ct) =>
                    mediator.Send(new GetMyAccountsQuery(
                        status,
                        accountFather,
                        accounttype,
                        pageNumber <= 0 ? 1 : pageNumber,
                        pageSize <= 0 ? 20 : Math.Min(pageSize, 100)), ct))
            .WithName("GetMyAccounts")
            .WithSummary("List Accounts for the current tenant");
    }
}
