using Completeapi.CsharpModel.Domain.Enums;

namespace Completeapi.CsharpModel.WebApi.Features.Users.UpdateUser;

/// <summary>
/// API response model for UpdateUser operation
/// </summary>
public class UpdateUserResponse
{
    /// <summary>
    /// The unique identifier of the updated user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's full name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public UserRole? Role { get; set; }

    /// <summary>
    /// The current status of the user
    /// </summary>
    public UserStatus? Status { get; set; }
}
