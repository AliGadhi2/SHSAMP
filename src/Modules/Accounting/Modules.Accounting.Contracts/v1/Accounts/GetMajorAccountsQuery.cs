using FSH.Framework.Shared.Persistence;
using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
public sealed record GetMajorAccountsQuery(
    AccountStatus? status,
    int? BranchID = 1,
    int? AccountType = 0,
     
    int PageNumber = 1,
    int PageSize = 20) : IQuery<IReadOnlyList<MajorAccountsDto>>;