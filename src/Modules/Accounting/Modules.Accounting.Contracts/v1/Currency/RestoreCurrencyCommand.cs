using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Currencies;

public sealed record RestoreCurrencyCommand(Guid CurrencyId) : ICommand<Guid>;
