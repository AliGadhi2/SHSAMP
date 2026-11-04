
namespace FSH.Modules.Accounting.Contracts.Dtos;

public sealed record CashReceiptDto(
    Guid Id,
    long CashReceiptNumber,
    DateTime TheDate,
    decimal Amount,
    string? Notes,
    Guid AccountID,
    Guid ExchangeAccountID,
    Guid CurrencyID,
    string? AccountName,
    string? ExchangeAccountName,
    string? CurrencyName
);