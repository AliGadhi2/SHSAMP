namespace FSH.Modules.Accounting.Contracts.Dtos;
public sealed record CurrencyDto(
    Guid Id,
    string Name,
    string Code,
    string Symbol,
    string? Notes
);