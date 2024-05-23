using Carter;
using Mapster;
using MediatR;
using TopUp.Application;

namespace TopUp.Api;

public record TopUpBeneficiaryRequest(TopUpBeneficiaryDto TopUpBeneficiary);
public record TopUpBeneficiaryResponse(bool IsSuccess);

public class TopUpBeneficiary : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/beneficiaries/top-up", async (TopUpBeneficiaryRequest request, ISender sender) =>
        {
            var command = request.Adapt<TopUpBeneficiaryCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<TopUpBeneficiaryResponse>();

            return Results.Ok(response);
        })
        .WithName("TopUpBeneficiary")
        .Produces<TopUpBeneficiaryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("TopUp Beneficiary")
        .WithDescription("TopUp Beneficiary");
    }
}