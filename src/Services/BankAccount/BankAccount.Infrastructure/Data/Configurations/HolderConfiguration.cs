using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankAccount.Domain;

namespace BankAccount.Infrastructure;

public class HolderConfiguration : IEntityTypeConfiguration<Holder>
{
    public void Configure(EntityTypeBuilder<Holder> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasConversion(
                HolderId => HolderId.Value,
                dbId => HolderId.From(dbId));

        builder.ComplexProperty(h => h.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            nameBuilder.Property(n => n.LastName)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.ComplexProperty(h => h.Email, emailBuilder =>
        {
            emailBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Holder.Email))
                        .IsRequired();
        });

        builder.ComplexProperty(h => h.Phone, phoneBuilder =>
        {
            phoneBuilder.Property(p => p.Value)
                .HasColumnName(nameof(Holder.Phone))
                .HasMaxLength(13)
                .IsRequired();
        });

        builder.ComplexProperty(h => h.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });
    }
}
