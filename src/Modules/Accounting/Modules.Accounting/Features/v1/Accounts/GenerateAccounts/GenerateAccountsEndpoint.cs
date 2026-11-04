using FSH.Framework.Shared.Identity.Authorization;
using FSH.Framework.Web.Idempotency;
using FSH.Modules.Accounting.Contracts.Authorization;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.Accounting.Features.v1.Accounts.GenerateAccounts;

public static class GenerateAccountEndpoint
{
    internal static RouteHandlerBuilder MapGenerateAccountEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/Accounts/generate",
                async (GenerateAcountCommand command, IMediator mediator, CancellationToken ct) =>
                    Results.Ok(new { generated = await mediator.Send(command, ct) }))
            .WithName("GenerateAccount")
            .WithSummary("Create an account for the current tenant")
            .RequirePermission(AccountingPermissions.Accounts.Create)
            .WithIdempotency();
    }
}
