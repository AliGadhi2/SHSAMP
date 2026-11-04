using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;

public sealed record UpdateAccountCommand(
    Guid AccountId,
    string AccountName,
    long AccountFather,
    int AccountType,
    string AccountRefrense,
    string? Notes) : ICommand<Guid>;
