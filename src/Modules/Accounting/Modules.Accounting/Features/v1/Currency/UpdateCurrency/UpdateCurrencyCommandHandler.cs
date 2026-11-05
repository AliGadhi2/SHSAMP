using System.Net;
using FSH.Framework.Core.Exceptions;
using FSH.Modules.Accounting.Contracts.v1.Currencies;
using FSH.Modules.Accounting.Data;
 
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Features.v1.Currencies.UpdateBrand;

public sealed class UpdateBrandCommandHandler(AccountingDbContext dbContext)
    : ICommandHandler<UpdateCurrencyCommand, Guid>
{
    public async ValueTask<Guid> Handle(UpdateCurrencyCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var currency = await dbContext.Currencies
            .FirstOrDefaultAsync(b => b.Id == command.CurrencyId, cancellationToken)
            .ConfigureAwait(false)
            ?? throw new NotFoundException($"Brand {command.CurrencyId} not found.");

        currency.Update(command.CurrencyId, command.Name, command.IsoCode);

        bool SameNAme = await dbContext.Currencies
            .AnyAsync(b => b.CurrencyName == currency.CurrencyName && b.Id != currency.Id, cancellationToken)
            .ConfigureAwait(false);
        if (SameNAme)
        {
            throw new CustomException(
                $"Another currency with name '{command.Name}' already exists.",
                (IEnumerable<string>?)null,
                HttpStatusCode.Conflict);
        }

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return currency.Id;
    }
}
