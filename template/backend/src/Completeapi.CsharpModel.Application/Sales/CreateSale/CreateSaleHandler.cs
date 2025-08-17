using AutoMapper;
using FluentValidation;
using MediatR;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.Domain.Enums;
using Completeapi.CsharpModel.Common.Security;

namespace Completeapi.CsharpModel.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwt;

    public CreateSaleHandler(
        ISaleRepository saleRepository, 
        IMapper mapper,
        IUserRepository userRepository,
        IJwtTokenGenerator jwt)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _jwt = jwt;

    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        UserInfo userInfo = _jwt.GetUserInfoFromToken(command.Token);
        if (userInfo == null|| userInfo.Id != command.CustomerId.ToString())
        {
            throw new InvalidOperationException("Somente o cliente pode fazer pedidos em seu nome.");
        }
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        User branch = await _userRepository.GetByIdAsync(command.BranchId);
        if (branch == null || ((int)branch.Role < (int)UserRole.Manager))
        {
            throw new InvalidOperationException("Vendedor inválido");
        }
        command.BranchName = branch.Username;

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);

        // Aplica regras de negócio do Sale
        foreach (var item in sale.Items)
            item.CalculateDiscount();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
