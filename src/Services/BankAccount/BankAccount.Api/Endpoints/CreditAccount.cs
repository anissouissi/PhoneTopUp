using Carter;
using Mapster;
using MediatR;
using BankAccount.Application;

namespace BankAccount.Api;

public record CreditAccountRequest(AccountTransactionDto Transaction);
public record CreditAccountResponse(bool IsSuccess);

public class CreditAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts/credit", async (CreditAccountRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreditAccountCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreditAccountResponse>();

            return Results.Ok(response);
        })
        .WithName("CreditAccount")
        .Produces<CreditAccountResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Credit Account")
        .WithDescription("Credit Account");
    }
}