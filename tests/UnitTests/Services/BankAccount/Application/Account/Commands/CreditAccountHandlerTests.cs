using AutoFixture;
using BankAccount.Application;
using BankAccount.Domain;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit.Categories;

namespace UnitTests;

[UnitTest("BankAccount")]
public class CreditAccountHandlerTests
{
    private readonly Fixture _fixture;

    public CreditAccountHandlerTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Should_CreditAccount_When_ValidCommand()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();
        var account = Account.Create(
            AccountId.From(Guid.NewGuid()),
            AccountNumber.From(Guid.NewGuid()),
            HolderId.From(Guid.NewGuid()),
            Balance.From(100));
        var accounts = new List<Account> { account }.AsQueryable().BuildMockDbSet();
        dbContext.Accounts.Returns(accounts);

        var command = _fixture.Build<CreditAccountCommand>()
            .With(x => x.Transaction, new AccountTransactionDto(account.AccountNumber.Value, 10)).Create();

        var handler = new CreditAccountHandler(dbContext);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
        account.Balance.Should().Be(Balance.From(110));
    }

    [Fact]
    public async Task Should_ThrowAccountNotFoundException_When_AccountDoseNotExist()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();
        var accounts = new List<Account>().AsQueryable().BuildMockDbSet();
        dbContext.Accounts.Returns(accounts);

        var command = _fixture.Create<CreditAccountCommand>();

        var handler = new CreditAccountHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AccountNotFoundException>()
                .WithMessage($"Entity \"Account\" ({command.Transaction.AccountNumber}) was not found.");
    }
}
