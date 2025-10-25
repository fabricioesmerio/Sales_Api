using Sales.Domain.Entities;

namespace Sales.Tests.Domain
{
    public class SaleTest
    {
        [Fact]
        public void AddItem_ShouldApply10PercentDiscount_WhenQuantityBetween4And9()
        {
            var sale = new Sale();
            var item = new SaleItem() { Product = "Beer", Quantity = 5, UnitPrice = 10m };

            sale.AddItem(item);

            Assert.Equal(0.10m, item.Discount);
            Assert.Equal(5 * 10m * (1 - 0.10m), item.Total);
        }

        [Fact]
        public void AddItem_ShouldApply20PercentDiscount_WhenQuantityBetween10And20()
        {
            var sale = new Sale();
            var item = new SaleItem() { Product = "Notebook", Quantity = 15, UnitPrice = 200m };

            sale.AddItem(item);

            Assert.Equal(0.20m, item.Discount);
        }

        [Fact]
        public void AddItem_ShouldNotAllowMoreThan20Items()
        {
            var sale = new Sale();
            var item = new SaleItem() { Product = "Test", Quantity = 30, UnitPrice = 1m };

            var exception = Assert.Throws<InvalidOperationException>(() => sale.AddItem(item));

            Assert.Equal("Cannot sell more than 20 identical items.", exception.Message);
        }

        [Fact]
        public void AddItem_ShouldNotApplyDiscount_WhenQuantityLessThan4()
        {
            var sale = new Sale();
            var item = new SaleItem() { Product = "Test", Quantity = 2, UnitPrice = 100m };

            sale.AddItem(item);

            Assert.Equal(0.00m, item.Discount);
        }

        [Fact]
        public void Cancel_ShouldMarkSaleAsCancelled()
        {
            var sale = new Sale();
            sale.Cancel();

            Assert.True(sale.IsCancelled);
        }

        [Fact]
        public void UpdateDetails_ShouldThrow_WhenSaleIsCancelled()
        {
            var sale = new Sale();
            sale.Cancel();

            var ex = Assert.Throws<InvalidOperationException>(() => sale.UpdateDetails("0001","Customer", "Test 2", DateTime.Now));

            Assert.Equal("Cannot update a cancelled sale.", ex.Message);
        }

        [Fact]
        public void UpdateItems_ShouldAddNewItem_WhenItemNotExists()
        {
            var sale = new Sale();
            var items = new SaleItem() { Product = "Laptop", Quantity = 6, UnitPrice = 10m};

            sale.UpdateItems(new() { items });

            Assert.Contains("Laptop", sale.Items.Find(i => i.Product == "Laptop")?.Product);
        }

        [Fact]
        public void UpdateItems_ShouldUpdateExistingItem()
        {
            var sale = new Sale();
            var items = new SaleItem() { Id = Guid.NewGuid(), Product = "Test", Quantity = 5, UnitPrice = 100m };
            sale.AddItem(items);

            var updateItems = new SaleItem() { Id = items.Id, Product = "Test", Quantity = 10, UnitPrice = 100m };

            sale.UpdateItems(new() { updateItems });

            Assert.Equal(0.20m, sale.Items.First().Discount);
        }

    }
}
