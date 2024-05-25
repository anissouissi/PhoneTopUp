using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TopUp.Application;

public class BankAccountServiceHttpClient(HttpClient httpClient, IConfiguration configuration) : IBankAccountServiceHttpClient
{
    public async Task DebitAccount(BankAccountTransactionDto transaction)
    {
        try
        {
            var url = configuration["BankAccountServiceUrl"] + "/accounts/debit";
            var response = await httpClient.PostAsJsonAsync(url, new DebitAccountRequest(transaction));
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new DebitAccountException(problem?.Detail ?? "Debit bank account failed");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
