using AutoMapper;
using MediatR;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.WebApi.Common;
using System.Linq.Expressions;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Enums;

namespace Completeapi.CsharpModel.Application.Sales.ListSale;

public class ListSaleHandler : IRequestHandler<ListSaleCommand, PaginatedList<ListSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwt;

    public ListSaleHandler(
        ISaleRepository saleRepository, 
        IMapper mapper,
        IJwtTokenGenerator jwt
        )
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _jwt = jwt;
    }

    public async Task<PaginatedList<ListSaleResult>> Handle(ListSaleCommand command, CancellationToken cancellationToken)
    {
        // 3️⃣ Validação de usuário e permissões
        UserInfo userInfo = _jwt.GetUserInfoFromToken(command.Token);
        if (userInfo == null)
            throw new InvalidOperationException("Usuário não permitido a fazer operação");
        string roleFromToken = userInfo.Role;

        // 1️⃣ Converter string para enum
        if (!Enum.TryParse<UserRole>(roleFromToken, out var roleEnum))
        {
            throw new InvalidOperationException("Role inválida");
        }

        // 2️⃣ Converter enum para int/short
        int roleValue = (int)roleEnum; // ou short roleValue = (short)roleEnum

        // 3️⃣ Validar permissões
        if (roleValue < (int)UserRole.Manager)
        {
            throw new InvalidOperationException("Usuário não permitido a fazer operação");
        }
        // 2️⃣ Buscar com inclusão automática de navegações
        var sales = _saleRepository.GetAllWhen(s =>
              (string.IsNullOrWhiteSpace(command.SaleNumber) || s.SaleNumber.Contains(command.SaleNumber)) &&
              (!command.CustomerId.HasValue || s.CustomerId == command.CustomerId.Value) &&
              (!command.BranchId.HasValue || s.BranchId == command.BranchId.Value) &&
              (!command.StartDate.HasValue || s.Date >= command.StartDate.Value) &&
              (!command.EndDate.HasValue || s.Date <= command.EndDate.Value)
          )
          .OrderByDescending(s => s.Date)
          .ToList();


        // 3️⃣ Paginar em memória (já que GetAllWhen retorna IEnumerable)
        var totalCount = sales.Count;
        var items = sales
            .Skip((command.PageNumber - 1) * command.PageSize)
            .Take(command.PageSize)
            .ToList();

        // 4️⃣ Mapear para DTO
        var mappedItems = items.Select(s => _mapper.Map<ListSaleResult>(s)).ToList();

        return await Task.FromResult(new PaginatedList<ListSaleResult>(
            mappedItems,
            totalCount,
            command.PageNumber,
            command.PageSize
        ));
    }
}
