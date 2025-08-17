using MediatR;
using FluentValidation;
using Completeapi.CsharpModel.Domain.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Common.Security;

namespace Completeapi.CsharpModel.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator jwt;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="validator">The validator for DeleteUserCommand</param>
    public DeleteUserHandler(
        IUserRepository userRepository, 
        IJwtTokenGenerator jwt
        )
    {
        _userRepository = userRepository;
        this.jwt = jwt;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request,CancellationToken cancellationToken)
    {
        var validator = new DeleteUserValidator();
        var userInfo = jwt.GetUserInfoFromToken(request.Token);
        if (userInfo == null ||
        (userInfo.Id != request.Id.ToString() &&
         (int)Enum.Parse(typeof(UserRole), userInfo.Role) < (int)UserRole.Manager))
        {
            throw new UnauthorizedAccessException("Acesso proibido: Apenas o pr�prio usu�rio ou um gerente podem realizar esta opera��o.");
        }
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _userRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");

        return new DeleteUserResponse { Success = true };
    }
}
