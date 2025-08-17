namespace Completeapi.CsharpModel.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Request model for deleting a user
/// </summary>
public class DeleteUserRequest
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The token from user to validade
    /// </summary>
    public string? Token { get; set; }
}
