using FSH.Framework.Shared.Persistence;
using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Modules.Accounting.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
public sealed class GetMajorAccountsQueryHandler(AccountingDbContext context) : IQueryHandler<GetMajorAccountsQuery, IReadOnlyList<MajorAccountsDto>>
{
     

    

    public async ValueTask<IReadOnlyList<MajorAccountsDto>> Handle(GetMajorAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts =await context.Accounts.Where(x => x.AccountType == AccountType.Major && x.BranchID == request.BranchID).AsQueryable()
       .Select(x => new MajorAccountsDto(
                x.Id,
                x.AccountNumber,
                x.AccountName,
                x.AccountReference
            ))
       
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false)
        ;
        return accounts ;

        
    }
}