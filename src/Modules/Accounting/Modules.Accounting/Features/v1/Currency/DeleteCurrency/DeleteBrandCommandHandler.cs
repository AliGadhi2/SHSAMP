using FSH.Framework.Core.Exceptions;
using FSH.Modules.Accounting.Contracts.v1.Currencies;
using FSH.Modules.Accounting.Data;
using FSH.Modules.Catalog.Data;
using Mediator;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace FSH.Modules.Accounting.Features.v1.Currencies.DeleteCurrency;

public sealed class DeleteCurrencyCommandHandler(AccountingDbContext dbContext)
    : ICommandHandler<DeleteCurrencyCommand, Unit>
{
    public async ValueTask<Unit> Handle(DeleteCurrencyCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var currency = await dbContext.Currencies
            .FirstOrDefaultAsync(c => c.Id == command.CurrencyId, cancellationToken)
            .ConfigureAwait(false)
            ?? throw new NotFoundException($"Currency {command.CurrencyId} not found.");

        dbContext.Currencies.Remove(currency);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Unit.Value;
    }

   
}
