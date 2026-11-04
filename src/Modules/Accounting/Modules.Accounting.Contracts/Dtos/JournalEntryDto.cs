using System.Collections.ObjectModel;

namespace FSH.Modules.Accounting.Contracts.Dtos;
public sealed record JournalEntryDto(
    Guid Id,
    long JournalNumber,
    DateTime TheDate,
    decimal Amount,
    string? Notes,
    Guid AccountID,
    Guid ExchangeAccountID,
    Guid CurrencyID,
    string? AccountName,
    string? ExchangeAccountName,
    string? CurrencyName,
    Collection<JournalLineDto> JournalEntryDetails
);