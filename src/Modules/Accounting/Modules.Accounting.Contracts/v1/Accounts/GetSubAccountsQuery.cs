using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
public sealed record GetSubAccountsQuery(
     
    int? BranchID = 1
   
     
    ) : IQuery<IReadOnlyList<SubAccountsDto>>;