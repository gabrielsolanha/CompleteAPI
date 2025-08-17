using AutoMapper;
using MediatR;
using FluentValidation;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Enums;

namespace Completeapi.CsharpModel.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwt;

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateUserCommand</param>
    public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
    }

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if ((int)command.Role >= (int)UserRole.Manager)
        {
            var userInfo = _jwt.GetUserInfoFromToken(command.Token);
            if (userInfo == null ||
             (int)Enum.Parse(typeof(UserRole), userInfo.Role) < (int)UserRole.Manager)
            {
                throw new UnauthorizedAccessException("Acesso proibido: Apenas um gerente podem realizar esta operação.");
            }
        }

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException($"User with email {command.Email} already exists");

        var user = _mapper.Map<User>(command);
        user.Password = _passwordHasher.HashPassword(command.Password);

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
        var result = _mapper.Map<CreateUserResult>(createdUser);
        return result;
    }
}
