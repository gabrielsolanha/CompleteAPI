using AutoMapper;

namespace Completeapi.CsharpModel.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands
/// </summary>
public class DeleteUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteUser feature
    /// </summary>
    public DeleteUserProfile()
    {
        CreateMap<DeleteUserRequest, Application.Users.DeleteUser.DeleteUserCommand>()
            .ConstructUsing(i => new Application.Users.DeleteUser.DeleteUserCommand(i.Id, i.Token));
    }
}
