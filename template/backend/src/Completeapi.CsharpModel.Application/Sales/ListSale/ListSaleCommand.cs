using Completeapi.CsharpModel.Common.Validation;
using Completeapi.CsharpModel.WebApi.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Completeapi.CsharpModel.Application.Sales.ListSale;

public class ListSaleCommand : IRequest<PaginatedList<ListSaleResult>>
{
    public string? Token { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Filtros
    public string? SaleNumber { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? BranchId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new ListSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}