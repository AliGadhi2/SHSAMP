using FluentValidation;
using FSH.Modules.Accounting.Contracts.v1.Accounts;

namespace FSH.Modules.Accounting.Features.v1.Accounts.GenerateAccounts;

public sealed class GenerateAccountsCommandValidator : AbstractValidator<GenerateAcountCommand>
{
    public GenerateAccountsCommandValidator()
    {
        RuleFor(x => x.AccountName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.AccountRefrense)
            .Must(reference => reference is "الميزانية العمومية" or "حساب الارباح والخسائر")
            .WithMessage("Account reference is invalid.");
        RuleFor(x => x.AccountType).InclusiveBetween(1, 2);
        RuleFor(x => x.AccountFather).GreaterThanOrEqualTo(0);
    }
}
