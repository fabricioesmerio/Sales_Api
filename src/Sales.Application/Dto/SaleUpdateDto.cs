
namespace Sales.Application.Dto
{
    public class SaleUpdateDto
    {
        public string SaleNumber { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Customer { get; set; } = default!;
        public string Branch { get; set; } = default!;
        public List<SaleItemUpdateDto> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
