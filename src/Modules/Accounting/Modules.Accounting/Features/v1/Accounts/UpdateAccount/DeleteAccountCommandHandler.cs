using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Services;
using Mediator;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public sealed class DeleteAccountCommandHandler(IAccountingService accounting)
    : ICommandHandler<DeleteAccountCommand, Guid>
{
    public async ValueTask<Guid> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        await accounting.DeleteAccountAsync(command.AccountId, cancellationToken).ConfigureAwait(false);
        return command.AccountId;
    }
}
