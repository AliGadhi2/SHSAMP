using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Services;
using Mediator;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public sealed class MarkAccountstopCommandHandler(IAccountingService billing)
    : ICommandHandler<MekAccountStopCommand, Guid>
{
    public async ValueTask<Guid> Handle(MekAccountStopCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        await billing.MarkAccountStopAsync(command.accountId, command.Reason, cancellationToken).ConfigureAwait(false);
        return command.accountId;
    }
}
