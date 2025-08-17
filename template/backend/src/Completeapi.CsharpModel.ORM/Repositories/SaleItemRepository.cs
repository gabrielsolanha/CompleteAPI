using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Completeapi.CsharpModel.ORM.Repositories;

/// <summary>
/// Implementation of ISaleItemRepository using Entity Framework Core
/// </summary>
public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleItemRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new saleItem in the database
    /// </summary>
    /// <param name="saleItem">The saleItem to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created saleItem</returns>
    public async Task<SaleItem> CreateAsync(
        SaleItem saleItem,
        CancellationToken cancellationToken = default
    )
    {
        await _context.SaleItems.AddAsync(saleItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItem;
    }

    /// <summary>
    /// Retrieves a saleItem by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the saleItem</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The saleItem if found, null otherwise</returns>
    public async Task<SaleItem?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.SaleItems.FirstOrDefaultAsync(
            o => o.Id == id,
            cancellationToken
        );
    }

    public async Task<List<SaleItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a saleItem from the database
    /// </summary>
    /// <param name="id">The unique identifier of the saleItem to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the saleItem was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var saleItem = await GetByIdAsync(id, cancellationToken);
        if (saleItem == null)
            return false;

        _context.SaleItems.Remove(saleItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
