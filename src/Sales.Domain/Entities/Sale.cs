

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

            ApplyDiscount(item);

            
            Items.Add(item);            
        }

        public void UpdateDetails(string number, string customer, string branch, DateTime date)
        {
            if (IsCancelled)
                throw new InvalidOperationException("Cannot update a cancelled sale.");
            
            SaleNumber = number;
            Customer = customer;
            Branch = branch;
            Date = date;
        }

        public void UpdateItems(List<SaleItem> updatedItems)
        {
            foreach (var item in updatedItems)
            {
                var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Product = item.Product;
                    existingItem.Quantity = item.Quantity;
                    existingItem.UnitPrice = item.UnitPrice;

                    ApplyDiscount(existingItem);
                }
                else
                {
                    AddItem(item);
                }
            }

            var toRemove = Items.Where(i => !updatedItems.Any(u => u.Id == i.Id)).ToList();
            foreach (var item in toRemove)
                Items.Remove(item);
        }

        public void Cancel() => IsCancelled = true;

        private void ApplyDiscount(SaleItem item)
        {
            if (item.Quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            if (item.Quantity >= 10)
                item.Discount = 0.20m;
            else if (item.Quantity >= 4)
                item.Discount = 0.10m;
            else
                item.Discount = 0.00m;
        }
    }
}
