using System.Threading;
using System.Threading.Tasks;
using Completeapi.CsharpModel.Application.Users.DeleteUser;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Completeapi.CsharpModel.Unit.Application.Users;

public class DeleteUserHandlerTests
{
    private readonly IUserRepository _repo = Substitute.For<IUserRepository>();
    private readonly IJwtTokenGenerator _jwt = Substitute.For<IJwtTokenGenerator>();
    private readonly DeleteUserHandler _handler;

    public DeleteUserHandlerTests()
    {
        _handler = new DeleteUserHandler(_repo, _jwt);
    }

    [Fact(DisplayName = "Given valid token and id When delete Then returns success=true")]
    public async Task DeleteUser_Success()
    {
        var id = Guid.NewGuid();
        var cmd = new DeleteUserCommand(id, "valid-token");

        _repo.DeleteAsync(id, Arg.Any<CancellationToken>()).Returns(true);
        UserInfo infojwt = new() { Id = id.ToString() };
        _jwt.GetUserInfoFromToken("valid-token").Returns(infojwt);

        var res = await _handler.Handle(cmd, CancellationToken.None);

        res.Success.Should().BeTrue();
    }
}
