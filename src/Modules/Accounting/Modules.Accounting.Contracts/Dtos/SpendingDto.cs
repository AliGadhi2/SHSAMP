namespace FSH.Modules.Accounting.Contracts.Dtos;
public sealed record SpendingDto(
    Guid Id,
    long SpendingNumber,
    DateTime TheDate,
    decimal Amount,
    decimal ExchangeRate,
    string? AccountName,
    string? ExchangeAccountName,
    string? CurrencyName,
     string? Notes,
      Guid CurrencyID, 
    Guid AccountID,
    Guid ExchangeAccountID
);