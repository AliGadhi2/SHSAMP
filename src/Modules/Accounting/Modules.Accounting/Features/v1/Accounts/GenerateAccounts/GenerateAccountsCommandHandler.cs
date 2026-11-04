using FSH.Framework.Core.Exceptions;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Services;
using Mediator;
using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Framework.Core.Context;
namespace FSH.Modules.Accounting.Features.v1.Accounts.GenerateAccounts;

public sealed class GenerateAccountCommandHandler( 
    IAccountingService Accounting)
    : ICommandHandler<GenerateAcountCommand, AccountDto>
{   
    public async ValueTask<AccountDto> Handle(GenerateAcountCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);  
        return await Accounting.GenerateAccountAsync(
        command.AccountName,
       
        command. AccountFather,
        command.AccountType,
        command.Notes ?? "",
        command.AccountRefrense
     ,cancellationToken).ConfigureAwait(false);
    }
}
