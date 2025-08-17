using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Domain.Validation;
using FluentValidation;

namespace Completeapi.CsharpModel.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Validator for UpdateUserRequest that defines validation rules for user creation.
/// </summary>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public UpdateUserRequestValidator()
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