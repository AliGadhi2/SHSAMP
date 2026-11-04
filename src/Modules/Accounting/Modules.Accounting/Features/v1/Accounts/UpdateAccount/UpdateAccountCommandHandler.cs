using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Services;
using Mediator;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public sealed class UpdateAccountCommandHandler(IAccountingService accounting)
    : ICommandHandler<UpdateAccountCommand, Guid>
{
    public async ValueTask<Guid> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        await accounting.UpdateAccountAsync(
            command.AccountId,
            command.AccountName,
            command.AccountFather,
            command.AccountType,
            command.AccountRefrense,
            command.Notes,
            cancellationToken).ConfigureAwait(false);
        return command.AccountId;
    }
}
