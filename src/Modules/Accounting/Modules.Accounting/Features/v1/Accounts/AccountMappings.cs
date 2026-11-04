using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Modules.Accounting.Domain;

namespace FSH.Modules.Accounting.Features.v1.Accounts;

internal static class AccountMappings
{
    public static AccountDto ToDto(this Account account) => new(
        account.Id,
        account.AccountNumber,
        account.AccountName,
        account.AccountFather, 
        account.Status,
        account.CreatedAtUtc,
        account.AccountType,
        account.Notes
       );
}
