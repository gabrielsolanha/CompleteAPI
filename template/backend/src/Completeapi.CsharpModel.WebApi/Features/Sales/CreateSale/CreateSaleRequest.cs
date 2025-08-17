using Completeapi.CsharpModel.Application.Sales.CreateSale;
using Completeapi.CsharpModel.Domain.Enums;

namespace Completeapi.CsharpModel.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    ///    Id do consumidor
    /// </summary>
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? Token { get; set; } = string.Empty;
    /// <summary>
    ///    Id da distribuidora tem q ser pelo menos manager
    /// </summary>
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;

    public List<CreateSaleItemDto> Items { get; set; } = new();
    public SaleStatus Status { get; set; } = SaleStatus.Active;
}
