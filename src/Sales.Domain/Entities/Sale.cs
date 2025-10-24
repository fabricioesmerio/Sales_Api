

namespace Sales.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Customer { get; set; } = default!;
        public string Branch { get; set; } = default!;
        public bool IsCancelled { get; set; }

        public decimal TotalAmount => Items.Sum(i => i.Total);
        public List<SaleItem> Items { get; set; } = new();

        public void AddItem(SaleItem item)
        {
            // Regras de negócio de quantidade e desconto
            if (item.Quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            if (item.Quantity >= 10)
                item.Discount = 0.20m;
            else if (item.Quantity >= 4)
                item.Discount = 0.10m;
            else
                item.Discount = 0.00m;

            Items.Add(item);
        }

        public void Cancel() => IsCancelled = true;
    }
}
