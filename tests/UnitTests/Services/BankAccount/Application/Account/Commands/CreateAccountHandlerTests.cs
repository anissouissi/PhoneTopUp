using AutoFixture;
using BankAccount.Application;
using BuildingBlocks;
using FluentAssertions;
using NSubstitute;
using Xunit.Categories;

namespace UnitTests;

[UnitTest("BankAccount")]
public class CreateAccountHandlerTests
{
    private readonly Fixture _fixture;

    public CreateAccountHandlerTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Should_ReturnAccountNumber_When_ValidCommand()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();

        var holder = _fixture.Build<HolderDto>()
            .With(x => x.Email, "valid@email.com")
            .With(x => x.Phone, "+971520000000")
            .With(x => x.ZipCode, "00000")
            .Create();
        var account = _fixture.Build<CreateAccountDto>()
            .With(x => x.Holder, holder)
            .With(x => x.Balance, 1000)
            .Create();
        var command = _fixture.Build<CreateAccountCommand>().With(x => x.Account, account).Create();

        var handler = new CreateAccountHandler(dbContext);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.AccountNumber.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Should_ValidationException_When_InvalidEmail()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();

        var holder = _fixture.Build<HolderDto>()
            .With(x => x.Email, "notValidEmail")
            .With(x => x.Phone, "+971520000000")
            .With(x => x.ZipCode, "00000")
            .Create();
        var account = _fixture.Build<CreateAccountDto>()
            .With(x => x.Holder, holder)
            .With(x => x.Balance, 1000)
            .Create();
        var command = _fixture.Build<CreateAccountCommand>().With(x => x.Account, account).Create();

        var handler = new CreateAccountHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>().WithMessage(ValidationMessages.EmailInvalid);
    }

    [Fact]
    public async Task Should_ValidationException_When_InvalidPhone()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();

        var holder = _fixture.Build<HolderDto>()
            .With(x => x.Email, "valid@email.com")
            .With(x => x.Phone, "0000000")
            .With(x => x.ZipCode, "00000")
            .Create();
        var account = _fixture.Build<CreateAccountDto>()
            .With(x => x.Holder, holder)
            .With(x => x.Balance, 1000)
            .Create();
        var command = _fixture.Build<CreateAccountCommand>().With(x => x.Account, account).Create();

        var handler = new CreateAccountHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>().WithMessage(ValidationMessages.PhoneInvalid);
    }

    [Fact]
    public async Task Should_ValidationException_When_InvalidZipCode()
    {
        var dbContext = Substitute.For<IApplicationDbContext>();

        var holder = _fixture.Build<HolderDto>()
            .With(x => x.Email, "valid@email.com")
            .With(x => x.Phone, "+971520000000")
            .With(x => x.ZipCode, "000000000000000")
            .Create();
        var account = _fixture.Build<CreateAccountDto>()
            .With(x => x.Holder, holder)
            .With(x => x.Balance, 1000)
            .Create();
        var command = _fixture.Build<CreateAccountCommand>().With(x => x.Account, account).Create();

        var handler = new CreateAccountHandler(dbContext);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>().WithMessage(ValidationMessages.ZipCodeInvalid);
    }
}
