using FluentValidation;
using BuildingBlocks;

namespace TopUp.Application;

public record TopUpBeneficiaryCommand(TopUpBeneficiaryDto TopUpBeneficiary)
    : ICommand<TopUpBeneficiaryResult>;

public record TopUpBeneficiaryResult(bool IsSuccess);

public class TopUpBeneficiaryCommandValidator : AbstractValidator<TopUpBeneficiaryCommand>
{
    public TopUpBeneficiaryCommandValidator()
    {
        RuleFor(x => x.TopUpBeneficiary.BeneficiaryId).NotEmpty().WithMessage("BeneficiaryId is required");
        RuleFor(x => x.TopUpBeneficiary.UserId).NotNull().WithMessage("UserId is required");
        RuleFor(x => x.TopUpBeneficiary.Amount).NotNull().WithMessage("Amount is required");
    }
}