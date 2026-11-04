using FSH.Framework.Shared.Identity.Authorization;
using FSH.Framework.Web.Idempotency;
using FSH.Modules.Accounting.Contracts.Authorization;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public static class MarkAccountStopEndpoint
{
    public sealed record IssueInvoiceBody(string resone);

    internal static RouteHandlerBuilder MapMarkAccountStopEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/accounts/stop",
                async (  MekAccountStopCommand command, IMediator mediator, CancellationToken ct) =>
                    Results.Ok(await mediator.Send( command, ct)))
            .WithName("StopAccount")
            .WithSummary("stoping a Account")
            .RequirePermission(AccountingPermissions.Accounts.View)
            .WithIdempotency();
    }
}
