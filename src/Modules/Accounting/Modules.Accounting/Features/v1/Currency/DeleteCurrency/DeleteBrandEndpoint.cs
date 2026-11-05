using FSH.Modules.Catalog.Contracts.Authorization;
using FSH.Framework.Shared.Identity.Authorization;
using FSH.Modules.Catalog.Contracts.v1.Brands;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using FSH.Modules.Accounting.Contracts.v1.Currencies;
using FSH.Modules.Accounting.Contracts.Authorization;
using FSH.Modules.Accounting.Data;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Features.v1.Currencies.DeleteCurrency;

public static class DeleteCurrencyEndpoint
{
    internal static RouteHandlerBuilder MapDeleteCurrencyEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/currencies/{currencyId:guid}",
                async (Guid currencyId, IMediator mediator, AccountingDbContext dbContext,CancellationToken ct) =>
                {
                    var foundEntrylineForCurrency=await dbContext.JournalLines.AsNoTracking().AnyAsync(x=>x.CurrencyId==currencyId,ct);
               if(foundEntrylineForCurrency)
        {
              return Results.NotFound("لا يمكن حذف عملة لها معاملات مرتبطة.");
        }
                    await mediator.Send(new DeleteCurrencyCommand(currencyId), ct);
                    return Results.NoContent();
                })
            .WithName("DeleteCurrency")
            .WithSummary("Delete a currency")
            .RequirePermission(AccountingPermissions.Currencies.Delete);
    }
}
