using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TopUp.Infrastructure;
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
        await SeedUsersAsync(context);
        await SeedBeneficiariesWithTopUpsAsync(context);
    }

    private static async Task SeedUsersAsync(ApplicationDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            await context.Users.AddRangeAsync(InitialData.Users);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedBeneficiariesWithTopUpsAsync(ApplicationDbContext context)
    {
        if (!await context.Beneficiaries.AnyAsync())
        {
            await context.Beneficiaries.AddRangeAsync(InitialData.BeneficiariesWithTopUps);
            await context.SaveChangesAsync();
        }
    }
}
