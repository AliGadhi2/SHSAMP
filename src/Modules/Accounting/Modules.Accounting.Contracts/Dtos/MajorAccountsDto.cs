using FSH.Modules.Accounting.Contracts;
namespace FSH.Modules.Accounting.Contracts.Dtos;

public sealed record MajorAccountsDto(
    Guid Id,
    long AccountNumber,
    string AccountName,
    AccountReference AccountReference

);