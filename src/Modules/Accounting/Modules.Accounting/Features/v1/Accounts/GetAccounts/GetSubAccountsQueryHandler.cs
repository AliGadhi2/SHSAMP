using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Modules.Accounting.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
public sealed class GetSubAccountsQueryHandler(AccountingDbContext context) : IQueryHandler<GetSubAccountsQuery, IReadOnlyList<SubAccountsDto>>
{
   

 

    public async ValueTask<IReadOnlyList<SubAccountsDto>> Handle(GetSubAccountsQuery request, CancellationToken cancellationToken)
    {
         ArgumentNullException.ThrowIfNull(request);
        var accounts = await context.Accounts
    .Where(x => x.AccountType == AccountType.Sub && x.BranchID == request.BranchID)
    .AsNoTracking()
    .OrderBy(c => c.AccountName)
    .Select(x => new SubAccountsDto(
        x.Id,
        x.AccountNumber,
        x.AccountName,
        x.AccountFather.AccountName,
        x.AccountReference,
        x.Email,
        x.Status
    ))
    .ToListAsync(cancellationToken)
    .ConfigureAwait(false);
    return  accounts;

        

           
    }

    
}