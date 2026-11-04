namespace FSH.Modules.Accounting.Contracts.Dtos;
public sealed record JournalLineDto(
    Guid Id,
    DateTime TheDate,
    decimal Amount,
    decimal MoneyAmount,
    decimal ExchangeRate,
    string? AccountName,
    string? ExchangeAccountName,
    string? CurrencyName,
     string? Notes,
      Guid CurrencyID, 
    Guid AccountID,
    Guid ExchangeAccountID
);