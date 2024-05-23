using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankAccount.Domain;

namespace BankAccount.Infrastructure;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasConversion(
                        AccountId => AccountId.Value,
                        dbId => AccountId.From(dbId));

        builder.HasOne<Holder>()
          .WithMany()
          .HasForeignKey(a => a.HolderId)
          .IsRequired();

        builder.Property(a => a.AccountNumber).HasConversion(
                        AccountNumber => AccountNumber.Value,
                        dbAccountNumber => AccountNumber.From(dbAccountNumber));

        builder.HasIndex(a => a.AccountNumber)
            .IsUnique();

        builder.ComplexProperty(a => a.Balance, balanceBuilder =>
            {
                balanceBuilder.Property(b => b.Value)
                    .HasColumnName(nameof(Account.Balance))
                    .IsRequired();
            });
    }
}
