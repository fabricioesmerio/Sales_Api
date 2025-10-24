

namespace Sales.Application.Dto
{
    public class SaleDto
    {
        public string SaleNumber { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Customer { get; set; } = default!;
        public string Branch { get; set; } = default!;
        public bool IsCancelled { get; set; }
        public List<SaleItemDto> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
