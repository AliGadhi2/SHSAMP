using System.Net;
using FSH.Framework.Core.Exceptions;
using FSH.Modules.Accounting.Contracts.v1.Currencies;
using FSH.Modules.Accounting.Data;
using FSH.Modules.Accounting.Domain;
using FSH.Modules.Catalog.Data;
using FSH.Modules.Catalog.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Features.v1.Currencies.CreateCurrency;

public sealed class CreateCurrencyCommandHandler(AccountingDbContext dbContext)
    : ICommandHandler<CreateCurrencyCommand, Guid>
{
    public async ValueTask<Guid> Handle(CreateCurrencyCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var currency = Currency.Create(command.Name, command.IsoCode);

        bool slugTaken = await dbContext.Currencies
            .AnyAsync(c => c.CurrencyName == currency.CurrencyName, cancellationToken)
            .ConfigureAwait(false);
        if (slugTaken)
        {
            throw new CustomException(
                $"A currency with name '{command.Name}' already exists.",
                (IEnumerable<string>?)null,
                HttpStatusCode.Conflict);
        }

        dbContext.Currencies.Add(currency);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return currency.Id;
    }
}
