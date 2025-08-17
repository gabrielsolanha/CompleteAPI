using Completeapi.CsharpModel.Common.Validation;
using Completeapi.CsharpModel.Domain.Enums;
using MediatR;

namespace Completeapi.CsharpModel.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; } // id de quem realiza a chamada seja de venda e/ou compra
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public List<UpdateSaleItemDto> Items { get; set; } = new();
    public SaleStatus Status { get; set; } = SaleStatus.Active;

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

public class UpdateSaleItemDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
