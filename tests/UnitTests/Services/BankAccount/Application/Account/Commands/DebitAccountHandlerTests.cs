using AutoFixture;
using BankAccount.Application;
using BankAccount.Domain;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit.Categories;

namespace UnitTests;

[UnitTest("BankAccount")]
public class DebitAccountHandlerTests
{
    private readonly Fixture _fixture;

    public DebitAccountHandlerTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Should_DebitAccount_When_ValidCommand()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();
        var account = Account.Create(
            AccountId.From(Guid.NewGuid()),
            AccountNumber.From(Guid.NewGuid()),
            HolderId.From(Guid.NewGuid()),
            Balance.From(100));
        var accounts = new List<Account> { account }.AsQueryable().BuildMockDbSet();
        dbContext.Accounts.Returns(accounts);

        var command = _fixture.Build<DebitAccountCommand>()
            .With(x => x.Transaction, new AccountTransactionDto(account.AccountNumber.Value, 10)).Create();

        var handler = new DebitAccountHandler(dbContext);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
        account.Balance.Should().Be(Balance.From(90));
    }

    [Fact]
    public async Task Should_ThrowInvalidOperationException_When_InsufficientFunds()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();
        var account = Account.Create(
            AccountId.From(Guid.NewGuid()),
            AccountNumber.From(Guid.NewGuid()),
            HolderId.From(Guid.NewGuid()),
            Balance.From(100));
        var accounts = new List<Account> { account }.AsQueryable().BuildMockDbSet();
        dbContext.Accounts.Returns(accounts);

        var command = _fixture.Build<DebitAccountCommand>()
            .With(x => x.Transaction, new AccountTransactionDto(account.AccountNumber.Value, 200)).Create();

        var handler = new DebitAccountHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Insufficient funds.");
    }

    [Fact]
    public async Task Should_ThrowAccountNotFoundException_When_AccountDoseNotExist()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();
        var accounts = new List<Account>().AsQueryable().BuildMockDbSet();
        dbContext.Accounts.Returns(accounts);

        var command = _fixture.Create<DebitAccountCommand>();

        var handler = new DebitAccountHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AccountNotFoundException>()
                .WithMessage($"Entity \"Account\" ({command.Transaction.AccountNumber}) was not found.");
    }
}