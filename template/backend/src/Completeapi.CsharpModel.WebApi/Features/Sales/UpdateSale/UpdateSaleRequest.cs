using Completeapi.CsharpModel.Application.Sales.UpdateSale;
using Completeapi.CsharpModel.Domain.Enums;

namespace Completeapi.CsharpModel.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a new sale in the system.
/// </summary>
public class UpdateSaleRequest
{
    public Guid Id { get; set; } // id da venda
    public string SaleNumber { get; set; } = string.Empty;
    /// <summary>
    ///    Id do consumidor
    /// </summary>
    public Guid CustomerId { get; set; } 
    public string CustomerName { get; set; } = string.Empty;
    public string? Token { get; set; } = string.Empty;
    /// <summary>
    ///    Id da distribuidora
    /// </summary>
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;

    public List<UpdateSaleItemDto> Items { get; set; } = new();
    public SaleStatus Status { get; set; } = SaleStatus.Active;
}
