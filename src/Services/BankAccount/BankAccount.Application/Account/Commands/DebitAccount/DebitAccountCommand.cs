using FluentValidation;
using BuildingBlocks;

namespace BankAccount.Application;

public record DebitAccountCommand(AccountTransactionDto Transaction)
    : ICommand<DebitAccountResult>;

public record DebitAccountResult(bool IsSuccess);

public class DebitAccountCommandValidator : AbstractValidator<DebitAccountCommand>
{
    public DebitAccountCommandValidator()
    {
        RuleFor(x => x.Transaction.AccountNumber).NotEmpty().WithMessage("AccountNumber is required");
        RuleFor(x => x.Transaction.Amount).NotNull().WithMessage("Amount is required");
    }
}