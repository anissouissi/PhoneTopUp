using Carter;
using Mapster;
using MediatR;
using BankAccount.Application;

namespace BankAccount.Api;

public record CreateAccountRequest(CreateAccountDto Account);
public record CreateAccountResponse(string AccountNumber);

public class CreateAccount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts", async (CreateAccountRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateAccountCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateAccountResponse>();

            return Results.Created($"/accounts/{response.AccountNumber}", response);
        })
        .WithName("CreateAccount")
        .Produces<CreateAccountResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Account")
        .WithDescription("Create Account");
    }
}