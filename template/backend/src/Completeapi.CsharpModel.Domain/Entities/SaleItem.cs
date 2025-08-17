using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Completeapi.CsharpModel.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; private set; }
        public decimal Total => Math.Round((Quantity * UnitPrice) - Discount, 2);
        public Guid SaleId { get; set; }
        public required Sale Sale { get; set; }

        public void CalculateDiscount()
        {
            if (Quantity >= 10 && Quantity <= 20)
                Discount = Quantity * UnitPrice * 0.20m;
            else if (Quantity >= 4)
                Discount = Quantity * UnitPrice * 0.10m;
            else if (Quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 units of a product.");
            else
                Discount = 0;
        }
    }

}
