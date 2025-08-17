using Completeapi.CsharpModel.Domain.Entities;

namespace Completeapi.CsharpModel.Domain.Repositories;

/// <summary>
/// Repository interface for SaleItem entity operations
/// </summary>
public interface ISaleItemRepository
{
    /// <summary>
    /// Creates a new saleItem in the repository
    /// </summary>
    /// <param name="saleItem">The saleItem to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created saleItem</returns>
    Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a saleItem by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the saleItem</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The saleItem if found, null otherwise</returns>
    Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all saleItems
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The saleItems if found, null otherwise</returns>
    Task<List<SaleItem>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a saleItem from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the saleItem to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the saleItem was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
