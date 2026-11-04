using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Accounts;

public sealed record MekAccountStopCommand(Guid accountId, string? Reason = null) : ICommand<Guid>;
