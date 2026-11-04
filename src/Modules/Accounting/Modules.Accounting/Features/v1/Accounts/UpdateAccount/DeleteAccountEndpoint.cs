using FSH.Framework.Shared.Identity.Authorization;
using FSH.Modules.Accounting.Contracts.Authorization;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public static class DeleteAccountEndpoint
{
    internal static RouteHandlerBuilder MapDeleteAccountEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/accounts/{accountId:guid}",
                async (Guid accountId, IMediator mediator, CancellationToken ct) =>
                    Results.Ok(await mediator.Send(new DeleteAccountCommand(accountId), ct)))
            .WithName("DeleteAccount")
            .WithSummary("Delete a leaf account")
            .RequirePermission(AccountingPermissions.Accounts.Manage);
    }
}
