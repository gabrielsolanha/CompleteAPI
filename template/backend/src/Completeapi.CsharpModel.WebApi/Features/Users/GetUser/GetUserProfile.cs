using AutoMapper;
using Completeapi.CsharpModel.Application.Users.GetUser;

namespace Completeapi.CsharpModel.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping between Application and API GetUser responses
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<GetUserRequest, GetUserCommand>();
        CreateMap<GetUserResult, GetUserResponse>();
    }
}
