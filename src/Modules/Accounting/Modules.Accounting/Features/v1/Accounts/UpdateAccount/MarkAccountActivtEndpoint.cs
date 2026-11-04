using FSH.Framework.Shared.Identity.Authorization;
using FSH.Framework.Web.Idempotency;
using FSH.Modules.Accounting.Contracts.Authorization;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public static class MarkAccountActivtEndpoint
{
    public sealed record IssueInvoiceBody(string resone);

    internal static RouteHandlerBuilder MapMarkAccountActivEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/accounts/activ",
                async (  MarkAccountActiveCommand command, IMediator mediator, CancellationToken ct) =>
                    Results.Ok(await mediator.Send(command, ct)))
            .WithName("activAccount")
            .WithSummary("activatid a Account")
            .RequirePermission(AccountingPermissions.Accounts.View)
            .WithIdempotency();
    }
}
