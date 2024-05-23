using FluentValidation;
using BuildingBlocks;

namespace BankAccount.Application;

public record DeleteAccountCommand(Guid AccountId)
    : ICommand<DeleteAccountResult>;

public record DeleteAccountResult(bool IsSuccess);

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("AccountId is required");
    }
}
