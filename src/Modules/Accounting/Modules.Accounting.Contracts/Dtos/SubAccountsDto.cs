using FSH.Modules.Accounting.Contracts;
namespace FSH.Modules.Accounting.Contracts.Dtos;

public sealed record SubAccountsDto(
    Guid Id,
    long AccountNumber,
    string AccountName,
    string FatherAccountName,
   
    AccountReference AccountReference,
     string Email,
    AccountStatus Status
    
    
);