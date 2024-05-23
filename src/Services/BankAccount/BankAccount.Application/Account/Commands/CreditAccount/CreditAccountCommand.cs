using FluentValidation;
using BuildingBlocks;

namespace BankAccount.Application;

public record CreditAccountCommand(AccountTransactionDto Transaction)
    : ICommand<CreditAccountResult>;

public record CreditAccountResult(bool IsSuccess);

public class CreditAccountCommandValidator : AbstractValidator<CreditAccountCommand>
{
    public CreditAccountCommandValidator()
    {
        RuleFor(x => x.Transaction.AccountNumber).NotEmpty().WithMessage("AccountNumber is required");
        RuleFor(x => x.Transaction.Amount).NotNull().WithMessage("Amount is required");
    }
}