using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Completeapi.CsharpModel.Application.Users.UpdateUser;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Completeapi.CsharpModel.Unit.Application.Users;

public class UpdateUserHandlerTests
{
    private readonly IUserRepository _repo = Substitute.For<IUserRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IPasswordHasher _hasher = Substitute.For<IPasswordHasher>();
    private readonly IJwtTokenGenerator _jwt = Substitute.For<IJwtTokenGenerator>();
    private readonly UpdateUserHandler _handler;

    public UpdateUserHandlerTests()
    {
        _handler = new UpdateUserHandler(_repo, _mapper, _hasher, _jwt);
    }

    [Fact(DisplayName = "Given valid data When update user Then returns Id only")]
    public async Task UpdateUser_Success()
    {
        var idg = Guid.NewGuid();
        var id = idg.ToString();
        var cmd = new UpdateUserCommand
        {
            Id = id,
            Email = "user@example.com",
            Username = "newname",
            Phone = "+5511999999999",
            Role = UserRole.Manager,
            Status = UserStatus.Active,
            Password = "NewStrongPass1!"
        };

        var mapped = new User { Id = idg, Email = cmd.Email, Username = cmd.Username, Role = UserRole.Manager };
        var updated = new User { Id = idg, Email = cmd.Email, Username = cmd.Username, Role = UserRole.Manager };

        _mapper.Map<User>(cmd).Returns(mapped);
        _hasher.HashPassword(cmd.Password!).Returns("hashed");
        UserInfo infojwt = new() { Id = id.ToString() };
        _jwt.GetUserInfoFromToken("").Returns(infojwt);
        _repo.UpdateAsync(mapped, Arg.Any<CancellationToken>()).Returns(updated);
        _mapper.Map<UpdateUserResult>(updated).Returns(new UpdateUserResult { Id = idg });

        var res = await _handler.Handle(cmd, CancellationToken.None);

        res.Should().NotBeNull();
        res.Id.Should().Be(cmd.Id);
    }
}