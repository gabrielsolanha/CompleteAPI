using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Completeapi.CsharpModel.Application.Users.GetUser;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Completeapi.CsharpModel.Unit.Application.Users;

public class GetUserHandlerTests
{
    private readonly IUserRepository _repo = Substitute.For<IUserRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly GetUserHandler _handler;

    public GetUserHandlerTests()
    {
        _handler = new GetUserHandler(_repo, _mapper);
    }

    [Fact(DisplayName = "Given existing user When get by id Then returns mapped result")]
    public async Task GetUser_Success()
    {
        var id = Guid.NewGuid();
        var cmd = new GetUserCommand(id);

        var user = new User
        {
            Id = id,
            Email = "user@example.com",
            Username = "theuser",
            Phone = "+5511999999999",
            Role = UserRole.Customer,
            Status = UserStatus.Active
        };

        var result = new GetUserResult
        {
            Id = id,
            Email = user.Email,
            Username = user.Username,
            Phone = user.Phone,
            Role = (UserRole)user.Role,
            Status = (UserStatus)user.Status
        };

        _repo.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns(user);
        _mapper.Map<GetUserResult>(user).Returns(result);

        var res = await _handler.Handle(cmd, CancellationToken.None);

        res.Id.Should().Be(id);
        res.Email.Should().Be("user@example.com");
        res.Role.Should().Be(UserRole.Customer);
    }
}
    