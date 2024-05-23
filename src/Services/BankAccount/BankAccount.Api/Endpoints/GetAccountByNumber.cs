using Carter;
using Mapster;
using MediatR;
using BankAccount.Application;

namespace BankAccount.Api;

public record GetAccountByNumberResponse(AccountDto Account);

public class GetAccountByNumber : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/{accountNumber}", async (Guid accountNumber, ISender sender) =>
        {
            var result = await sender.Send(new GetAccountByNumberQuery(accountNumber));

            var response = result.Adapt<GetAccountByNumberResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAccountByNumber")
        .Produces<GetAccountByNumberResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Account By Number")
        .WithDescription("Get Account By Number");
    }
}
