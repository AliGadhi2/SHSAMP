using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;

public sealed record IssueAccountCommand(Guid accountId, string? Resone = null) : ICommand<Guid>;
