
namespace Sales.Application.Dto
{
    public class SaleItemUpdateDto
    {
        public Guid Id { get; set; }
        public string Product { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
