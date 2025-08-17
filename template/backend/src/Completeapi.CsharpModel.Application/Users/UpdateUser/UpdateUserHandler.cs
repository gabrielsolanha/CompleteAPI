using AutoMapper;
using MediatR;
using FluentValidation;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace Completeapi.CsharpModel.Application.Users.UpdateUser;

/// <summary>
/// Handler for processing UpdateUserCommand requests
/// </summary>
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwt;

    /// <summary>
    /// Initializes a new instance of UpdateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for UpdateUserCommand</param>
    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
    }

    /// <summary>
    /// Handles the UpdateUserCommand request
    /// </summary>
    /// <param name="command">The UpdateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user details</returns>
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        var userInfo = _jwt.GetUserInfoFromToken(command.Token);
        if (userInfo == null ||
        (userInfo.Id != command.Id.ToString() &&
         (int)Enum.Parse(typeof(UserRole), userInfo.Role) < (int)UserRole.Manager)
         )
        {
            throw new UnauthorizedAccessException("Acesso proibido: Apenas o próprio usuário ou um gerente podem realizar esta operação.");
        }

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = _mapper.Map<User>(command);
        if(!command.Password.IsNullOrEmpty())
            user.Password = _passwordHasher.HashPassword(command.Password);

        var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);
        var result = _mapper.Map<UpdateUserResult>(updatedUser);
        return result;
    }
}
