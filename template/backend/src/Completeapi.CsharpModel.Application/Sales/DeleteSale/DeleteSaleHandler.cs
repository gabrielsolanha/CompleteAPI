using MediatR;
using FluentValidation;
using Completeapi.CsharpModel.Domain.Repositories;
using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Completeapi.CsharpModel.Common.Security;

namespace Completeapi.CsharpModel.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwt;

    /// <summary>
    /// Initializes a new instance of DeleteSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="validator">The validator for DeleteSaleCommand</param>
    public DeleteSaleHandler(
        ISaleRepository saleRepository,
        IUserRepository userRepository,
        IJwtTokenGenerator jwt)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _jwt = jwt;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request
    /// </summary>
    /// <param name="request">The DeleteSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteSaleResponse> Handle(
        DeleteSaleCommand request,
        CancellationToken cancellationToken
    )
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Buscar a venda
        Sale sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException($"Sale with ID {request.Id} not found");
        UserInfo userInfo = _jwt.GetUserInfoFromToken(request.Token);
        if (userInfo == null)
        {
            throw new InvalidOperationException("Usuário não permitido a fazer operação");
        }
        User userLog = await _userRepository.GetByIdAsync(id: Guid.Parse(userInfo.Id), cancellationToken);
        if (userLog == null || (userInfo.Id != sale.BranchId.ToString() && userInfo.Id != sale.CustomerId.ToString() && ((int)userLog.Role < (int)UserRole.Admin)))
        {
            throw new InvalidOperationException("Usuário não permitido a fazer operação");
        }

        // Se já estiver cancelada, não precisa repetir a operação
        if (sale.IsCancelled)
            return new DeleteSaleResponse { Success = true };

        // Cancelar (regra de negócio)
        sale.Cancel();

        // Persistir a mudança
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return new DeleteSaleResponse { Success = true };
    }
}
