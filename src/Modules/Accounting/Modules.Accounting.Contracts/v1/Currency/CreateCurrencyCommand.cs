using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Currencies;

public sealed record CreateCurrencyCommand(
    string Name,
    string? IsoCode = null) : ICommand<Guid>;
