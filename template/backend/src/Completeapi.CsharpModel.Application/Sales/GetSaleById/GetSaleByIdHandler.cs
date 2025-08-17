using AutoMapper;
using MediatR;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.Common.Validation;
using Completeapi.CsharpModel.Common.Security;
using Completeapi.CsharpModel.Domain.Enums;

namespace Completeapi.CsharpModel.Application.Sales.GetSaleById;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdCommand, GetSaleByIdResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwt;

    public GetSaleByIdHandler(
        ISaleRepository saleRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IJwtTokenGenerator jwt)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _jwt = jwt;
    }

    public async Task<GetSaleByIdResult> Handle(GetSaleByIdCommand command, CancellationToken cancellationToken)
    {
        // Busca a venda pelo Id, incluindo os itens
        var sale = _saleRepository.GetAllWhen(s => s.Id == command.Id).FirstOrDefault();
        if (sale == null)
            throw new InvalidOperationException($"Sale with ID {command.Id} not found");
        // Validação de usuário e permissões
        UserInfo userInfo = _jwt.GetUserInfoFromToken(command.Token);
        if (userInfo == null)
            throw new InvalidOperationException("Usuário não permitido a fazer operação");

        User userLog = await _userRepository.GetByIdAsync(Guid.Parse(userInfo.Id), cancellationToken);
        bool isAdminOrManager = (int)userLog.Role.Value >= (int)UserRole.Admin;
        bool isSaleOwner = userInfo.Id == sale.BranchId.ToString() || userInfo.Id == sale.CustomerId.ToString();

        if (!isSaleOwner && !isAdminOrManager)
            throw new InvalidOperationException("Usuário não permitido a fazer operação");

        // Mapeia para DTO
        var result = _mapper.Map<GetSaleByIdResult>(sale);

        return await Task.FromResult(result);
    }
}
