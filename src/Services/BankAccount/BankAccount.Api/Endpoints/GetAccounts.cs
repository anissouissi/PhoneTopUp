using Carter;
using Mapster;
using MediatR;
using BankAccount.Application;

namespace BankAccount.Api;

public record GetAccountsResponse(IEnumerable<AccountDto> Accounts);

public class GetAccounts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAccountsQuery());

            var response = result.Adapt<GetAccountsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAccounts")
        .Produces<GetAccountsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Accounts")
        .WithDescription("Get Accounts");
    }
}
