using FluentValidation;

namespace Completeapi.CsharpModel.WebApi.Features.Sales.ListSale;

public class ListSaleRequestValidator : AbstractValidator<ListSaleRequest>
{
    public ListSaleRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than zero.");
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than zero.");
    }
}
