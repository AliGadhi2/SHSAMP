using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;

public sealed record UpdateAccountCommand(
    Guid AccountId,
    string AccountName,
    Guid AccountFather,
    AccountType AccountType,
    AccountReference AccountRefrense,
    string? Notes) : ICommand<Guid>;
