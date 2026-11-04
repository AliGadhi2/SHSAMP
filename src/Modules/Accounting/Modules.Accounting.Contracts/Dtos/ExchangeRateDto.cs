namespace FSH.Modules.Accounting.Contracts.Dtos;
public sealed record ExchangeRateDto(
    Guid Id,
    long ExchangeRateNumber,
    DateTime TheDate,
    decimal Rate,
    string? Notes,
    Guid CurrencyID,
    string? CurrencyName
);