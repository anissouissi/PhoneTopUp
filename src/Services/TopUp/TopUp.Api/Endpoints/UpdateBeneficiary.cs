using Carter;
using Mapster;
using MediatR;
using TopUp.Application;

namespace TopUp.Api;

public record UpdateBeneficiaryRequest(UpdateBeneficiaryDto Beneficiary);
public record UpdateBeneficiaryResponse(bool IsSuccess);

public class UpdateBeneficiary : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/beneficiaries", async (UpdateBeneficiaryRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateBeneficiaryCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateBeneficiaryResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateBeneficiary")
        .Produces<UpdateBeneficiaryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Beneficiary")
        .WithDescription("Update Beneficiary");
    }
}
