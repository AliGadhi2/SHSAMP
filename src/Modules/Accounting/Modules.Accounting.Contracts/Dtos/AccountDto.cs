 
using FSH.Modules.Accounting.Contracts;
namespace FSH.Modules.Accounting.Contracts.Dtos;

public sealed record AccountDto(
    Guid Id,
    long AccountNumber,
    string AccountName,
    string FatherAccountName,
   
    string AccountReference,
     string Email,
    AccountStatus Status,
    DateTime CreatedAtUtc,
    long AccountFather,
    AccountType AccountType,
    string? Notes
    
);
