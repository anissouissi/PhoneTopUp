using FluentValidation;
using BuildingBlocks;

namespace TopUp.Application;

public record CreateBeneficiaryCommand(CreateBeneficiaryDto Beneficiary)
    : ICommand<CreateBeneficiaryResult>;

public record CreateBeneficiaryResult(Guid Id);

public class CreateBeneficiaryCommandValidator : AbstractValidator<CreateBeneficiaryCommand>
{
    public CreateBeneficiaryCommandValidator()
    {
        RuleFor(x => x.Beneficiary.BeneficiaryNickname).NotEmpty().WithMessage("Nickname is required");
        RuleFor(x => x.Beneficiary.UserId).NotNull().WithMessage("UserId is required");
    }
}