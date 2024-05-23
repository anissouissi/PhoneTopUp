using FluentValidation;
using BuildingBlocks;

namespace TopUp.Application;

public record DeleteBeneficiaryCommand(Guid BeneficiaryId)
    : ICommand<DeleteBeneficiaryResult>;

public record DeleteBeneficiaryResult(bool IsSuccess);

public class DeleteBeneficiaryCommandValidator : AbstractValidator<DeleteBeneficiaryCommand>
{
    public DeleteBeneficiaryCommandValidator()
    {
        RuleFor(x => x.BeneficiaryId).NotEmpty().WithMessage("BeneficiaryId is required");
    }
}
