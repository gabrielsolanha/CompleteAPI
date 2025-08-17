using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Completeapi.CsharpModel.Domain.Common;
using Completeapi.CsharpModel.Domain.Enums;

namespace Completeapi.CsharpModel.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Sale()
        {
            CreatedAt = DateTime.UtcNow;
            Date = DateTime.UtcNow;
        }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Branch Warehouses the person/place responsible by the sale aproval/reproval it is also an User
        /// </summary>
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public List<SaleItem> Items { get; set; } = new();

        public bool IsCancelled => Status == SaleStatus.Cancelled;

        public SaleStatus Status { get; private set; } = SaleStatus.Active;

        public decimal TotalAmount => Items.Sum(i => i.Total);

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void AddItem(SaleItem item)
        {
            item.CalculateDiscount();
            Items.Add(item);
        }

        public void Cancel()
        {
            Status = SaleStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
