using Microsoft.EntityFrameworkCore;
using TopUp.Application;
using TopUp.Domain;
using System.Reflection;

namespace TopUp.Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Domain.TopUp> TopUps => Set<Domain.TopUp>();
    public DbSet<Beneficiary> Beneficiaries => Set<Beneficiary>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
