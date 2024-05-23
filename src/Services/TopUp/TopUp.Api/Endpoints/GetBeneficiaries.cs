using Carter;
using Mapster;
using MediatR;
using TopUp.Application;

namespace TopUp.Api;

public record GetBeneficiariesResponse(IEnumerable<BeneficiaryDto> Beneficiaries);

public class GetBeneficiaries : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/beneficiaries", async (ISender sender) =>
        {
            var result = await sender.Send(new GetBeneficiariesQuery());

            var response = result.Adapt<GetBeneficiariesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBeneficiaries")
        .Produces<GetBeneficiariesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Beneficiaries")
        .WithDescription("Get Beneficiaries");
    }
}
