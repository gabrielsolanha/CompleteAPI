using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Domain.Validation;
using FluentValidation;

namespace Completeapi.CsharpModel.Application.Users.UpdateUser;

/// <summary>
/// Validator for UpdateUserCommand that defines validation rules for user creation command.
/// </summary>
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Username: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public UpdateUserCommandValidator()
    {
            RuleFor(user => user.Email)
                .SetValidator(new EmailValidator())
                .When(user => !string.IsNullOrWhiteSpace(user.Email));

            RuleFor(user => user.Username)
                .Length(3, 50)
                .When(user => !string.IsNullOrWhiteSpace(user.Username));

            RuleFor(user => user.Password)
                .SetValidator(new PasswordValidator())
                .When(user => !string.IsNullOrWhiteSpace(user.Password));

            RuleFor(user => user.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(user => !string.IsNullOrWhiteSpace(user.Phone));

            RuleFor(user => user.Status)
                .NotEqual(UserStatus.Unknown)
                .When(user => user.Status != null);

            RuleFor(user => user.Role)
                .NotEqual(UserRole.None)
                .When(user => user.Role != null);
    }
}