using TopUp.Application;
using FluentAssertions;
using Xunit.Categories;
using TopUp.Domain;
using NSubstitute;

namespace UnitTests;

[UnitTest("TopUp")]
public class TopUpBeneficiaryHandlerTests(EfSqliteFixture dbFixture) : IClassFixture<EfSqliteFixture>
{
    private readonly EfSqliteFixture _dbFixture = dbFixture;

    [Fact]
    public async Task Should_ReturnSuccess_When_ValidCommand()
    {
        var dbContext = _dbFixture.TopUpDbContext;
        var user = User.Create(UserId.From(Guid.NewGuid()), "test", "+971526666666", AccountNumber.From(Guid.NewGuid()), true);
        var beneficiary = Beneficiary.Create(BeneficiaryId.From(Guid.NewGuid()), BeneficiaryNickname.From("B1"),
                Phone.From("+971526666667"), user.Id);
        dbContext.Users.Add(user);
        dbContext.Beneficiaries.Add(beneficiary);
        dbContext.SaveChanges();

        var bankAccountServiceHttpClientMock = Substitute.For<IBankAccountServiceHttpClient>();
        bankAccountServiceHttpClientMock.DebitAccount(Arg.Any<BankAccountTransactionDto>()).Returns(Task.CompletedTask);

        var command = new TopUpBeneficiaryCommand(new TopUpBeneficiaryDto(BeneficiaryId: beneficiary.Id.Value,
            UserId: user.Id.Value, Amount: 100, Fee: 1));

        var handler = new TopUpBeneficiaryHandler(dbContext, bankAccountServiceHttpClientMock);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
    }
}