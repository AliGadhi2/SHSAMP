using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
public static class GetAccountSubEndpoint
{
    public static RouteHandlerBuilder MapGetAccountSubEndpoint(this IEndpointRouteBuilder endpoints)
    {
      return  endpoints.MapGet("/api/v1/accounts/sub", async (GetSubAccountsQuery request, IMediator mediator) =>
        {
            IReadOnlyList<SubAccountsDto> result = await mediator.Send(request);
            return Results.Ok(result);
        })
        .WithName("GetAccountSub")
        .WithSummary("Get Account Sub.")
        .WithDescription("Get Account Sub.")
        .Produces<IReadOnlyList<SubAccountsDto>>(StatusCodes.Status200OK);

         
    }
}