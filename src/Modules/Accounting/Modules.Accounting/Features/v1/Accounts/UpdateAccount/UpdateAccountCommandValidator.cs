using FluentValidation;
using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Contracts.v1.Accounts;

namespace FSH.Modules.Accounting.Features.v1.Accounts.UpdateAccount;

public sealed class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(command => command.AccountId).NotEmpty();
        RuleFor(command => command.AccountName).NotEmpty().MaximumLength(200);
        RuleFor(command => command.AccountFather).GreaterThanOrEqualTo(0);
        RuleFor(command => command.AccountType).InclusiveBetween(1, 2);
        RuleFor(command => command.AccountRefrense)
            .Must(reference => reference is AccountReference.BalanceSheet or AccountReference.Statement)
            .WithMessage("Account reference is invalid.");
    }
}
