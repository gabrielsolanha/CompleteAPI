using Completeapi.CsharpModel.Application.Users.CreateUser;
using Completeapi.CsharpModel.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Completeapi.CsharpModel.WebApi.Mappings;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}