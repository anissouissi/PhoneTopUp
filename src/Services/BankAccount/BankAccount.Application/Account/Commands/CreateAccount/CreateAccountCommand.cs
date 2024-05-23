using FluentValidation;
using BuildingBlocks;

namespace BankAccount.Application;

public record CreateAccountCommand(CreateAccountDto Account)
    : ICommand<CreateAccountResult>;

public record CreateAccountResult(Guid AccountNumber);

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Account.Holder.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(x => x.Account.Holder.LastName).NotEmpty().WithMessage("LastName is required");
        RuleFor(x => x.Account.Holder.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Account.Holder.Phone).NotEmpty().WithMessage("Phone is required");
        RuleFor(x => x.Account.Holder.AddressLine).NotEmpty().WithMessage("AddressLine is required");
        RuleFor(x => x.Account.Holder.State).NotEmpty().WithMessage("State is required");
        RuleFor(x => x.Account.Holder.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(x => x.Account.Holder.ZipCode).NotEmpty().WithMessage("ZipCode is required");
        RuleFor(x => x.Account.Balance).NotNull().WithMessage("Balance is required")
                                       .GreaterThanOrEqualTo(0).WithMessage("Balance should be greater than or equal to zero");
    }
}