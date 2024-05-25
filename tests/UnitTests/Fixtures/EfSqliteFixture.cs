using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

public class EfSqliteFixture : IAsyncLifetime
{
    private const string ConnectionString = "Data Source=:memory:";
    private readonly SqliteConnection _connection;

    public EfSqliteFixture()
    {
        _connection = new SqliteConnection(ConnectionString);
        _connection.Open();

        var bankAccountBuilder = new DbContextOptionsBuilder<BankAccount.Infrastructure.ApplicationDbContext>()
            .AddInterceptors(new BankAccount.Infrastructure.AuditableEntityInterceptor())
            .UseSqlite(_connection);
        var topUpBuilder = new DbContextOptionsBuilder<TopUp.Infrastructure.ApplicationDbContext>()
            .AddInterceptors(new TopUp.Infrastructure.AuditableEntityInterceptor())
            .UseSqlite(_connection);

        BankAccountDbContext = new BankAccount.Infrastructure.ApplicationDbContext(bankAccountBuilder.Options);
        TopUpDbContext = new TopUp.Infrastructure.ApplicationDbContext(topUpBuilder.Options);
    }

    public BankAccount.Infrastructure.ApplicationDbContext BankAccountDbContext { get; }
    public TopUp.Infrastructure.ApplicationDbContext TopUpDbContext { get; }

    public Task DisposeAsync() => Task.CompletedTask;

    public async Task InitializeAsync()
    {
        await BankAccountDbContext.Database.EnsureDeletedAsync();
        await BankAccountDbContext.Database.EnsureCreatedAsync();

        await TopUpDbContext.Database.EnsureDeletedAsync();
        await TopUpDbContext.Database.EnsureCreatedAsync();
    }
}
