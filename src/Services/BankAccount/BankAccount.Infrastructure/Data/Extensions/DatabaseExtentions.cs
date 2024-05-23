using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccount.Infrastructure;
public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedHoldersAsync(context);
        await SeedAccountsAsync(context);
    }

    private static async Task SeedHoldersAsync(ApplicationDbContext context)
    {
        if (!await context.Holders.AnyAsync())
        {
            await context.Holders.AddRangeAsync(InitialData.Holders);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedAccountsAsync(ApplicationDbContext context)
    {
        if (!await context.Accounts.AnyAsync())
        {
            await context.Accounts.AddRangeAsync(InitialData.Accounts);
            await context.SaveChangesAsync();
        }
    }
}
