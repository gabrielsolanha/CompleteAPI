using MediatR;

namespace Completeapi.CsharpModel.Application.Sales.DeleteSale;

/// <summary>
/// Command for deleting a sale
/// </summary>
public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    public DeleteSaleCommand(Guid id, string token)
    {
        Id = id;
        Token = token;
    }

    /// <summary>
    /// The unique identifier of the sale to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// The unique identifier of the sale to delete
    /// </summary>
    public string Token { get; }
}
