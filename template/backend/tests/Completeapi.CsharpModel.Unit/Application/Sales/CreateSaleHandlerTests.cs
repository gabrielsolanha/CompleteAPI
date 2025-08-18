using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Completeapi.CsharpModel.Application.Sales.CreateSale;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Completeapi.CsharpModel.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepo = Substitute.For<ISaleRepository>();
    private readonly IUserRepository _userRepo = Substitute.For<IUserRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IJwtTokenGenerator _jwt = Substitute.For<IJwtTokenGenerator>();
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        // ordem correta: (ISaleRepository, IUserRepository, IMapper, IJwtTokenGenerator)
        _handler = new CreateSaleHandler(_saleRepo, (IMapper)_mapper, _userRepo, _jwt);
    }

    [Fact(DisplayName = "Given valid sale When create Then returns id")]
    public async Task CreateSale_Success()
    {
        var clientId = Guid.NewGuid();
        var bId = Guid.NewGuid();
        var cmd = new CreateSaleCommand
        {
            Id = clientId,
            CustomerId = clientId,
            CustomerName = "Acme",
            BranchId = bId,
            BranchName = "Main",
            Items = new List<CreateSaleItemDto>
            {
                new()
                {
                    ProductName = "P1",
                    Quantity = 2,
                    UnitPrice = 10
                },
                new()
                {
                    ProductName = "P2",
                    Quantity = 1,
                    UnitPrice = 20
                }
            }
        };
        var sale = new Sale
        {
            Id = cmd.Id,
            CustomerId = cmd.CustomerId,
            CustomerName = cmd.CustomerName,
            Items = new List<SaleItem>()
        };

        sale.Items.AddRange(
            new List<SaleItem>
            {
                new()
                {
                    ProductName = "P1",
                    Quantity = 2,
                    UnitPrice = 10m,
                    Sale = sale // 👈 obrigatório
                },
                new()
                {
                    ProductName = "P2",
                    Quantity = 1,
                    UnitPrice = 20m,
                    Sale = sale // 👈 obrigatório
                }
            }
        );
        UserInfo infojwt = new() { Id = clientId.ToString() };

        _mapper.Map<Sale>(cmd).Returns(sale);
        _saleRepo.CreateAsync(sale, Arg.Any<CancellationToken>()).Returns(sale);
        _jwt.GetUserInfoFromToken("").Returns(infojwt);
        _userRepo.GetByIdAsync(bId).Returns(new User {Id= bId, Role = UserRole.Admin, Username = bId.ToString() });
        _mapper.Map<CreateSaleResult>(sale).Returns(new CreateSaleResult { Id = sale.Id });

        var res = await _handler.Handle(cmd, CancellationToken.None);

        res.Id.Should().Be(cmd.Id);
    }
}
