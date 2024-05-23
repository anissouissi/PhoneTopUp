using Carter;
using Mapster;
using MediatR;
using TopUp.Application;

namespace TopUp.Api;

public record DeleteBeneficiaryResponse(bool IsSuccess);

public class DeleteBeneficiary : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/beneficiaries/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBeneficiaryCommand(Id));

            var response = result.Adapt<DeleteBeneficiaryResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteBeneficiary")
        .Produces<DeleteBeneficiaryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Beneficiary")
        .WithDescription("Delete Beneficiary");
    }
}
