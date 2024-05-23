using Carter;
using Mapster;
using MediatR;
using BankAccount.Application;

namespace BankAccount.Api;

public record DeleteAccountResponse(bool IsSuccess);

public class DeleteAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/accounts/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteAccountCommand(Id));

            var response = result.Adapt<DeleteAccountResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteAccount")
        .Produces<DeleteAccountResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Account")
        .WithDescription("Delete Account");
    }
}
