using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopUp.Domain;

namespace TopUp.Infrastructure;

public class TopUpConfiguration : IEntityTypeConfiguration<Domain.TopUp>
{
    public void Configure(EntityTypeBuilder<Domain.TopUp> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasConversion(
                        topUpId => topUpId.Value,
                        dbId => TopUpId.From(dbId));

        builder.HasOne<User>()
          .WithMany()
          .HasForeignKey(t => t.UserId)
          .IsRequired();

        builder.ComplexProperty(b => b.Amount, amountBuilder =>
            {
                amountBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Domain.TopUp.Amount))
                    .IsRequired();
            });

        builder.Property(b => b.Fee);
    }
}
