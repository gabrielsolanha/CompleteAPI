using Completeapi.CsharpModel.Domain.Entities;
using System.Linq.Expressions;

namespace Completeapi.CsharpModel.Domain.Repositories
{

    public interface ISaleRepository
    {
        IEnumerable<Sale> GetAllWhen(Expression<Func<Sale, bool>> predicado);
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }

}
