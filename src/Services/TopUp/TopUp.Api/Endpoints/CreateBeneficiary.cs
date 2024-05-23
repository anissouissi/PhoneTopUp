using Carter;
using Mapster;
using MediatR;
using TopUp.Application;

namespace TopUp.Api;

public record CreateBeneficiaryRequest(CreateBeneficiaryDto Beneficiary);
public record CreateBeneficiaryResponse(Guid Id);

public class CreateBeneficiary : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/beneficiaries", async (CreateBeneficiaryRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateBeneficiaryCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateBeneficiaryResponse>();

            return Results.Created($"/beneficiaries/{response.Id}", response);
        })
        .WithName("CreateBeneficiary")
        .Produces<CreateBeneficiaryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Beneficiary")
        .WithDescription("Create Beneficiary");
    }
}