using Microsoft.EntityFrameworkCore;
using BankAccount.Application;
using BankAccount.Domain;
using System.Reflection;

namespace BankAccount.Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Holder> Holders => Set<Holder>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
