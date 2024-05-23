using Microsoft.EntityFrameworkCore;
using TopUp.Domain;

namespace TopUp.Application;
public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Domain.TopUp> TopUps { get; }
    DbSet<Beneficiary> Beneficiaries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
