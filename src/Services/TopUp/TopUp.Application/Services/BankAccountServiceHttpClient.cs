using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace TopUp.Application;

public class BankAccountServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
{
    public async Task DebitAccount(BankAccountTransactionDto transaction)
    {
        try
        {
            var url = configuration["BankAccountServiceUrl"] + "/accounts/debit";
            var response = await httpClient.PostAsJsonAsync(url, new DebitAccountRequest(transaction));
            if (!response.IsSuccessStatusCode)
            {
                throw new DebitAccountException("Debit bank account failed");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
