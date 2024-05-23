using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopUp.Domain;

namespace TopUp.Infrastructure;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
                userId => userId.Value,
                dbId => UserId.From(dbId));

        builder.HasMany(u => u.Beneficiaries)
            .WithOne()
            .HasForeignKey(t => t.UserId);

        builder.ComplexProperty(u => u.AccountNumber, accountNumberBuilder =>
        {
            accountNumberBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(User.AccountNumber))
                        .IsRequired();
        });

        builder.ComplexProperty(u => u.Nickname, nicknameBuilder =>
        {
            nicknameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(User.Nickname))
                        .HasMaxLength(20)
                        .IsRequired();
        });

        builder.ComplexProperty(u => u.Phone, phoneBuilder =>
        {
            phoneBuilder.Property(p => p.Value)
                .HasColumnName(nameof(User.Phone))
                .HasMaxLength(13)
                .IsRequired();
        });

        builder.Property(u => u.Verified).HasDefaultValue(false);
    }
}
