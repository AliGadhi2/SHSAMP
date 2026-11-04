using FSH.Framework.Shared.Persistence;
using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
//return account where user have primmsions at it 
public sealed record GetMyAccountsQuery(
    AccountStatus? status,
    long? AccountFather = null,
    int? AccountType = null,
     
    int PageNumber = 1,
    int PageSize = 20) : IQuery<PagedResponse<AccountDto>>;
