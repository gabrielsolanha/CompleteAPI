using FluentValidation;

namespace Completeapi.CsharpModel.Application.Sales.ListSale;

public class ListSaleCommandValidator : AbstractValidator<ListSaleCommand>
{
    public ListSaleCommandValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than zero.");
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than zero.");
    }
}
