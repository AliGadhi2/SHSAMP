using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;

public sealed record DeleteAccountCommand(Guid AccountId) : ICommand<Guid>;
