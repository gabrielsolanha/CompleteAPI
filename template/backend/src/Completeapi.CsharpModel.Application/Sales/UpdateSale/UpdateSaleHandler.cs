using AutoMapper;
using FluentValidation;
using MediatR;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Common.Security;

namespace Completeapi.CsharpModel.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwt;

    public UpdateSaleHandler(
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

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        // 1️⃣ Validação inicial do comando
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // 2️⃣ Buscar venda existente
        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
            throw new InvalidOperationException($"Sale with ID {command.Id} not found");

        // 3️⃣ Validação de usuário e permissões
        UserInfo userInfo = _jwt.GetUserInfoFromToken(command.Token);
        if (userInfo == null)
            throw new InvalidOperationException("Usuário não permitido a fazer operação");

        User userLog = await _userRepository.GetByIdAsync(Guid.Parse(userInfo.Id), cancellationToken);
        bool isAdminOrManager = userLog != null && (int)userLog.Role >= (int)UserRole.Admin;
        bool isSaleOwner = userInfo.Id == existingSale.BranchId.ToString() || userInfo.Id == existingSale.CustomerId.ToString();

        if (!isSaleOwner && !isAdminOrManager)
            throw new InvalidOperationException("Usuário não permitido a fazer operação");
        if (userInfo == null || userInfo.Id != command.CustomerId.ToString())
        {
            throw new InvalidOperationException("Somente o cliente pode fazer pedidos em seu nome.");
        }

        // 4️⃣ Buscar e validar vendedor (Branch)
        User branch = await _userRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch == null || (int)branch.Role < (int)UserRole.Manager)
            throw new ValidationException("Vendedor inválido");

        command.BranchName = branch.Username;

        // 5️⃣ Mapear as alterações mantendo o ID
        var updatedSale = _mapper.Map<Sale>(command);
        updatedSale.Id = existingSale.Id; // garantir que não muda

        // 6️⃣ Regras de negócio: recalcular descontos
        foreach (var item in updatedSale.Items)
            item.CalculateDiscount();

        // 7️⃣ Persistir atualização
        var resultEntity = await _saleRepository.UpdateAsync(updatedSale, cancellationToken);

        // 8️⃣ Retornar resultado mapeado
        return _mapper.Map<UpdateSaleResult>(resultEntity);
    }
}
