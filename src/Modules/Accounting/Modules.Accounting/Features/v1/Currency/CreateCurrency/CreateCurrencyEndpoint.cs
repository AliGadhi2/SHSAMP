using FSH.Framework.Shared.Identity.Authorization;
using FSH.Framework.Web.Idempotency;
using FSH.Modules.Accounting.Contracts.v1.Currencies;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using FSH.Modules.Accounting.Contracts.Authorization;

namespace FSH.Modules.Accounting.Features.v1.Currencies.CreateCurrency;

public static class CreateCurrencyEndpoint
{
    internal static RouteHandlerBuilder MapCreateCurrencyEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/currencies",
                async (CreateCurrencyCommand command, IMediator mediator, CancellationToken ct) =>
                    Results.Ok(await mediator.Send(command, ct)))
            .WithName("CreateCurrency")
            .WithSummary("Create a currency")
            .RequirePermission(AccountingPermissions.Currencies.Create)
            .WithIdempotency();
    }
}
