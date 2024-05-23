using FluentValidation;
using BuildingBlocks;

namespace TopUp.Application;
public record UpdateBeneficiaryCommand(UpdateBeneficiaryDto Beneficiary)
    : ICommand<UpdateBeneficiaryResult>;

public record UpdateBeneficiaryResult(bool IsSuccess);

public class UpdateBeneficiaryCommandValidator : AbstractValidator<UpdateBeneficiaryCommand>
{
    public UpdateBeneficiaryCommandValidator()
    {
        RuleFor(x => x.Beneficiary.BeneficiaryNickname).NotEmpty().WithMessage("Nickname is required");
        RuleFor(x => x.Beneficiary.Phone).NotNull().WithMessage("Phone is required");
    }
}

