namespace Completeapi.CsharpModel.WebApi.Features.Sales.ListSale;

public class ListSaleRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SaleNumber { get; set; }
    public string? Token { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? BranchId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
