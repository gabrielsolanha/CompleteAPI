using AutoMapper;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Application.Auth.AuthenticateUser;

namespace Completeapi.CsharpModel.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// AutoMapper profile for authentication-related mappings
/// </summary>
public sealed class AuthenticateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserProfile"/> class
    /// </summary>
    public AuthenticateUserProfile()
    {
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
        // CreateMap<User, Response>()
        //     .ForMember(dest => dest.Token, opt => opt.Ignore())
        //     .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}