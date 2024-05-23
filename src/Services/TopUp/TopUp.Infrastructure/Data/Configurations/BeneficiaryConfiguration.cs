using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopUp.Domain;

namespace TopUp.Infrastructure;

public class BeneficiaryConfiguration : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasConversion(
                        beneficiaryId => beneficiaryId.Value,
                        dbId => BeneficiaryId.From(dbId));

        builder.HasOne<User>()
          .WithMany()
          .HasForeignKey(b => b.UserId)
          .IsRequired();

        builder.HasMany(b => b.TopUps)
            .WithOne()
            .HasForeignKey(t => t.BeneficiaryId);

        builder.ComplexProperty(b => b.Nickname, nicknameBuilder =>
            {
                nicknameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Beneficiary.Nickname))
                    .HasMaxLength(20)
                    .IsRequired();
            });

        builder.ComplexProperty(b => b.Phone, phoneBuilder =>
            {
                phoneBuilder.Property(p => p.Value)
                    .HasColumnName(nameof(Beneficiary.Phone))
                    .HasMaxLength(13)
                    .IsRequired();
            });
    }
}
