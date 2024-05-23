using Carter;
using Mapster;
using MediatR;
using TopUp.Application;

namespace TopUp.Api;

public record GetBeneficiariesByUserResponse(IEnumerable<BeneficiaryDto> Beneficiaries);

public class GetBeneficiariesByUser : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/beneficiaries/user/{userId}", async (Guid userId, ISender sender) =>
        {
            var result = await sender.Send(new GetBeneficiariesByUserQuery(userId));

            var response = result.Adapt<GetBeneficiariesByUserResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBeneficiariesByUser")
        .Produces<GetBeneficiariesByUserResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Beneficiaries By User")
        .WithDescription("Get Beneficiaries By User");
    }
}
