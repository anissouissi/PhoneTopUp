using TopUp.Application;
using BuildingBlocks;
using FluentAssertions;
using Xunit.Categories;
using TopUp.Domain;

namespace UnitTests;

[UnitTest("TopUp")]
public class CreateBeneficiaryHandlerTests(EfSqliteFixture dbFixture) : IClassFixture<EfSqliteFixture>
{
    private readonly EfSqliteFixture _dbFixture = dbFixture;

    [Fact]
    public async Task Should_ReturnBeneficiaryId_When_ValidCommand()
    {
        var dbContext = _dbFixture.TopUpDbContext;
        var user = User.Create(UserId.From(Guid.NewGuid()), "test", "+971526666666", AccountNumber.From(Guid.NewGuid()), true);
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var command = new CreateBeneficiaryCommand(
            Beneficiary: new CreateBeneficiaryDto(UserId: user.Id.Value, BeneficiaryNickname: "B1", Phone: "+971527777777"));

        var handler = new CreateBeneficiaryHandler(dbContext);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Should_ThrowValidationException_When_InvalidNickname()
    {
        var dbContext = _dbFixture.TopUpDbContext;
        var user = User.Create(UserId.From(Guid.NewGuid()), "test", "+971526666666", AccountNumber.From(Guid.NewGuid()), true);
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var command = new CreateBeneficiaryCommand(
            Beneficiary: new CreateBeneficiaryDto(UserId: user.Id.Value, BeneficiaryNickname: "B11111111111111111111", Phone: "+971527777777"));

        var handler = new CreateBeneficiaryHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>().WithMessage(string.Format(ValidationMessages.NicknameMaxLength,
                DomainConstants.NicknameMaxLength));
    }

    [Fact]
    public async Task Should_ThrowValidationException_When_InvalidPhone()
    {
        var dbContext = _dbFixture.TopUpDbContext;
        var user = User.Create(UserId.From(Guid.NewGuid()), "test", "+971526666666", AccountNumber.From(Guid.NewGuid()), true);
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var command = new CreateBeneficiaryCommand(
            Beneficiary: new CreateBeneficiaryDto(UserId: user.Id.Value, BeneficiaryNickname: "B1", Phone: "11111111"));

        var handler = new CreateBeneficiaryHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>().WithMessage(ValidationMessages.PhoneInvalid);
    }

    [Fact]
    public async Task Should_ThrowDomainException_When_MaxAllowedBeneficiariesReached()
    {
        var dbContext = _dbFixture.TopUpDbContext;
        var user = User.Create(UserId.From(Guid.NewGuid()), "test", "+971526666666", AccountNumber.From(Guid.NewGuid()), true);
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        for (int i = 0; i < 5; i++)
        {
            var beneficiary = Beneficiary.Create(BeneficiaryId.From(Guid.NewGuid()), BeneficiaryNickname.From($"B{i + 1}"),
                Phone.From($"+97152666666{i + 1}"), user.Id);
            dbContext.Beneficiaries.Add(beneficiary);
        }
        dbContext.SaveChanges();

        var command = new CreateBeneficiaryCommand(
            Beneficiary: new CreateBeneficiaryDto(UserId: user.Id.Value, BeneficiaryNickname: "B6", Phone: "+971526666666"));

        var handler = new CreateBeneficiaryHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainException>().WithMessage(
            $"Domain Exception: \"{string.Format(DomainExceptionMessages.MaxAllowedBeneficiaries,
            DomainConstants.MaxAllowedBeneficiaries)}\" throws from Domain Layer.");
    }
}
