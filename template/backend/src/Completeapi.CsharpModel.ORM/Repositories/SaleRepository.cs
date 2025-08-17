using Completeapi.CsharpModel.Domain.Entities;
using Completeapi.CsharpModel.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Completeapi.CsharpModel.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }
        public async Task<Sale> CreateAsync(
            Sale sale,
            CancellationToken cancellationToken = default
        )
        {
            var currentYear = DateTime.Now.Year;

            // Contagem de vendas do ano atual
            var salesCount = await _context.Sales
                .Where(s => s.Date.Year == currentYear)
                .CountAsync(cancellationToken);

            // Gera o SaleNumber no formato "Venda nº [ANO]-[SEQ]"
            sale.SaleNumber = $"{currentYear}-{(salesCount + 1):D6}"; // Inicia o contador no próximo número

            // Adiciona a venda no banco de dados
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return sale;
        }

        public async Task<Sale?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(s => s.Items).ToListAsync(cancellationToken);
        }

        public IEnumerable<Sale> GetAllWhen(Expression<Func<Sale, bool>> predicado)
        {
            var query = _context.Set<Sale>().Where(predicado);

            var entityType = _context.Model.FindEntityType(typeof(Sale));
            var navigations = entityType.GetNavigations();

            foreach (var navigation in navigations)
            {
                query = query.Include(navigation.Name);
            }

            return query.ToList();
        }
        public async Task<Sale?> UpdateAsync(
            Sale sale,
            CancellationToken cancellationToken = default
        )
        {
            var existingSale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

            if (existingSale == null)
                return null;

            _context.Entry(existingSale).CurrentValues.SetValues(sale);

            // Remover itens que não existem mais
            var toRemove = existingSale.Items
                .Where(ei => !sale.Items.Any(i => i.Id == ei.Id))
                .ToList();
            _context.SaleItems.RemoveRange(toRemove);

            // Atualizar ou adicionar novos
            foreach (var item in sale.Items)
            {
                var existingItem = existingSale.Items.FirstOrDefault(ei => ei.Id == item.Id);
                if (existingItem != null)
                {
                    item.SaleId = existingItem.SaleId;
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
                }
                else
                {
                    item.SaleId = sale.Id;
                    existingSale.Items.Add(item);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales.FindAsync(new object[] { id }, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
