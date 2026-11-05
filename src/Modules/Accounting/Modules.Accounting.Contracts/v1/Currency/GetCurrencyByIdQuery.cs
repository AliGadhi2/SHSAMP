using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Currencies;

public sealed record GetCurrencyByIdQuery(Guid CurrencyId) : IQuery<CurrencyDto>;
