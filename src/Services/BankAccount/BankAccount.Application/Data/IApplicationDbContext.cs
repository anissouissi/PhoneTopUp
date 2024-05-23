using Microsoft.EntityFrameworkCore;
using BankAccount.Domain;

namespace BankAccount.Application;
public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; }
    DbSet<Holder> Holders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
