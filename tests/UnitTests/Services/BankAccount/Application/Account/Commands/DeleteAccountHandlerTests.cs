using AutoFixture;
using BankAccount.Application;
using BankAccount.Domain;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit.Categories;

namespace UnitTests;

[UnitTest("BankAccount")]
public class DeleteAccountHandlerTests
{
    private readonly Fixture _fixture;

    public DeleteAccountHandlerTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Should_DeleteAccount_When_ValidCommand()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();
        var account = Account.Create(
            AccountId.From(Guid.NewGuid()),
            AccountNumber.From(Guid.NewGuid()),
            HolderId.From(Guid.NewGuid()),
            Balance.From(100));
        var accounts = new List<Account> { account }.AsQueryable().BuildMockDbSet();
        dbContext.Accounts.Returns(accounts);

        var command = new DeleteAccountCommand(account.Id.Value);

        var handler = new DeleteAccountHandler(dbContext);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ThrowAccountNotFoundException_When_AccountDoseNotExist()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();
        var accounts = new List<Account>().AsQueryable().BuildMockDbSet();
        dbContext.Accounts.Returns(accounts);

        var command = _fixture.Create<DeleteAccountCommand>();

        var handler = new DeleteAccountHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AccountNotFoundException>()
                .WithMessage($"Entity \"Account\" ({command.AccountId}) was not found.");
    }
}
