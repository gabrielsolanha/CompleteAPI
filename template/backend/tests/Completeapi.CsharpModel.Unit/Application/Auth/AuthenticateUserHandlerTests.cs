using System.Threading;
using System.Threading.Tasks;
using Completeapi.CsharpModel.Application.Auth.AuthenticateUser;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Completeapi.CsharpModel.Unit.Application.Auth;

public class AuthenticateUserHandlerTests
{
    private readonly IUserRepository _userRepo = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher _hasher = Substitute.For<IPasswordHasher>();
    private readonly IJwtTokenGenerator _jwt = Substitute.For<IJwtTokenGenerator>();
    private readonly AuthenticateUserHandler _handler;

    public AuthenticateUserHandlerTests()
    {
        _handler = new AuthenticateUserHandler(_userRepo, _hasher, _jwt);
    }

    [Fact(DisplayName = "Given valid credentials When authenticate Then returns token")]
    public async Task Authenticate_Success()
    {
        var cmd = new AuthenticateUserCommand
        {
            Email = "user@example.com",
            Password = "P@ssw0rd!"
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "user@example.com",
            Username = "theuser",
            Phone = "+5511999999999",
            Role = UserRole.Customer,
            Status = UserStatus.Active,
            Password = "hashed"
        };

        _userRepo.GetByEmailAsync(cmd.Email, Arg.Any<CancellationToken>()).Returns(user);
        _hasher.VerifyPassword("P@ssw0rd!", "hashed").Returns(true);
        _jwt.GenerateToken(user).Returns("jwt-token");

        var res = await _handler.Handle(cmd, CancellationToken.None);

        res.Token.Should().Be("jwt-token");
        res.Email.Should().Be("user@example.com");
        res.Role.Should().Be(user.Role.ToString());
    }
    [Fact(DisplayName = "Given inactive user When authenticate Then throws UnauthorizedAccessException")]
    public async Task Authenticate_Fails_WhenUserInactive()
    {
        var cmd = new AuthenticateUserCommand
        {
            Email = "user@example.com",
            Password = "P@ssw0rd!"
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "user@example.com",
            Username = "theuser",
            Phone = "+5511999999999",
            Role = UserRole.Customer,
            Password = "hashed",
        };

        _userRepo.GetByEmailAsync(cmd.Email, Arg.Any<CancellationToken>()).Returns(user);
        _hasher.VerifyPassword("P@ssw0rd!", "hashed").Returns(true);

        var act = async () => await _handler.Handle(cmd, CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("User is not active");
    }

    [Fact(DisplayName = "Given wrong password When authenticate Then throws UnauthorizedAccessException")]
    public async Task Authenticate_WrongPassword()
    {
        var cmd = new AuthenticateUserCommand { Email = "user@example.com", Password = "wrong" };
        var user = new User { Email = "user@example.com", Password = "hashed", Role = UserRole.Customer };
        _userRepo.GetByEmailAsync(cmd.Email, Arg.Any<CancellationToken>()).Returns(user);
        _hasher.VerifyPassword("wrong", "hashed").Returns(false);

        await FluentActions
            .Invoking(() => _handler.Handle(cmd, CancellationToken.None))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }
}