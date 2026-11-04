using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Services;
using Mediator;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public sealed class MarkAccountActiveCommandHandler(IAccountingService billing)
    : ICommandHandler<MarkAccountActiveCommand, Guid>
{
    public async ValueTask<Guid> Handle(MarkAccountActiveCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        await billing.MarkAccountActiveAsync(command.accountId, command.Resone, cancellationToken).ConfigureAwait(false);
        return command.accountId;
    }
}
