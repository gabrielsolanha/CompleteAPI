using AutoMapper;
using Completeapi.CsharpModel.Application.Users.CreateUser;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.Unit.Domain;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Completeapi.CsharpModel.Unit.Application;

public class CreateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwt;
    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _jwt = Substitute.For<IJwtTokenGenerator>();

        _handler = new CreateUserHandler(_userRepository, _mapper, _passwordHasher, _jwt);
    }

    [Fact(DisplayName = "Given valid user data When creating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        command.Role = UserRole.Customer; // Não precisa validar permissão

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        var result = new CreateUserResult { Id = user.Id };

        _mapper.Map<User>(command).Returns(user);
        _mapper.Map<CreateUserResult>(user).Returns(result);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User)null);

        // When
        var createUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createUserResult.Should().NotBeNull();
        createUserResult.Id.Should().Be(user.Id);
        await _userRepository.Received(1).CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid user data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new CreateUserCommand(); // Campos vazios

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given existing email When creating user Then throws exception")]
    public async Task Handle_ExistingEmail_ThrowsInvalidOperationException()
    {
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        command.Role = UserRole.Customer;

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(new User());

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with email {command.Email} already exists");
    }

    [Fact(DisplayName = "Given Manager role without permission When creating user Then throws UnauthorizedAccessException")]
    public async Task Handle_ManagerRoleWithoutPermission_ThrowsUnauthorizedAccessException()
    {
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        command.Role = UserRole.Manager;
        command.Token = "fakeToken";

        // Retorna null para simular token inválido
        _jwt.GetUserInfoFromToken(command.Token).Returns((UserInfo)null);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Acesso proibido: Apenas um gerente podem realizar esta operação.");
    }

    [Fact(DisplayName = "Given user creation request When handling Then password is hashed")]
    public async Task Handle_ValidRequest_HashesPassword()
    {
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        command.Role = UserRole.Customer;
        var originalPassword = command.Password;
        const string hashedPassword = "h@shedPassw0rd";

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        _mapper.Map<User>(command).Returns(user);
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User)null);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.HashPassword(originalPassword).Returns(hashedPassword);

        await _handler.Handle(command, CancellationToken.None);

        _passwordHasher.Received(1).HashPassword(originalPassword);
        await _userRepository.Received(1).CreateAsync(
            Arg.Is<User>(u => u.Password == hashedPassword),
            Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        command.Role = UserRole.Customer;

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        _mapper.Map<User>(command).Returns(user);
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User)null);
        _userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        await _handler.Handle(command, CancellationToken.None);

        _mapper.Received(1).Map<User>(Arg.Is<CreateUserCommand>(c =>
            c.Username == command.Username &&
            c.Email == command.Email &&
            c.Phone == command.Phone &&
            c.Status == command.Status &&
            c.Role == command.Role));
    }
}
