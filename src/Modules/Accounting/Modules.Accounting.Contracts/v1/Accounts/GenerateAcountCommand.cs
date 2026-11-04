using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;
 
public sealed record GenerateAcountCommand(
    string AccountName,
    long AccountFather,
    int AccountType,
    string AccountRefrense,
    string? Notes) : ICommand<AccountDto>;
