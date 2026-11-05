using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
public static class GetAccountsMajorEndpoint
{
    public static RouteHandlerBuilder MapGetAccountsMajorEndpoint(this IEndpointRouteBuilder endpoints)
    {
      return  endpoints.MapGet("/api/v1/accounts/major", async (GetMajorAccountsQuery request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(request, ct);
            return Results.Ok(result);
        })
        .WithName("GetMajorAccounts")
        .WithSummary("Get Major Accounts")
        .WithDescription("Get Major Accounts.")
        .Produces<IReadOnlyList<MajorAccountsDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);
 
         
    }
}