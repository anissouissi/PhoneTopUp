using Carter;
using Mapster;
using MediatR;
using BankAccount.Application;

namespace BankAccount.Api;

public record DebitAccountRequest(AccountTransactionDto Transaction);
public record DebitAccountResponse(bool IsSuccess);

public class DebitAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/debit", async (DebitAccountRequest request, ISender sender) =>
        {
            var command = request.Adapt<DebitAccountCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<DebitAccountResponse>();

            return Results.Ok(response);
        })
        .WithName("DebitAccount")
        .Produces<DebitAccountResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Credit Account")
        .WithDescription("Credit Account");
    }
}