

namespace Sales.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }
        public string Product { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => Quantity * UnitPrice * (1 - Discount);
    }
}
