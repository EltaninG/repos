using System.Collections.Generic;
using System.Text.Json;
using StoreLibraries.Classes;
using StoreLibraries.Exceptions;
using Xunit;

namespace StoreLibraries.Test
{
    public class StoreTests
    {
        private Store TestStore { get; set; }


        private void Import()
        {
            Stocktaking Stock = new Stocktaking();
            Stock.Category.Add(new Category() { Discount = 0.1, Name = "Fantastique" });
            Stock.Category.Add(new Category() { Discount = 0.05, Name = "Science Fiction" });
            Stock.Category.Add(new Category() { Discount = 0.15, Name = "Philosophy" });

            Stock.Catalog.Add(new Book() { Name = "J.K Rowling - Goblet Of fire", Category = "Fantastique", Price = 8, Quantity = 2 });
            Stock.Catalog.Add(new Book() { Name = "Ayn Rand - FountainHead", Category = "Philosophy", Price = 12, Quantity = 10 });
            Stock.Catalog.Add(new Book() { Name = "Isaac Asimov - Foundation", Category = "Science Fiction", Price = 16, Quantity = 1 });
            Stock.Catalog.Add(new Book() { Name = "Isaac Asimov - Robot series", Category = "Science Fiction", Price = 5, Quantity = 2 });
            Stock.Catalog.Add(new Book() { Name = "Robin Hobb - Assassin Apprentice", Category = "Fantastique", Price = 12, Quantity = 8 });

            TestStore = new Store();
            TestStore.Import(JsonSerializer.Serialize(Stock));
        }

        
        
        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new string[] { "J.K Rowling - Goblet Of fire", "J.K Rowling - Goblet Of fire" }, 16 },
            new object[] { new string[] { "J.K Rowling - Goblet Of fire", "J.K Rowling - Goblet Of fire", "Robin Hobb - Assassin Apprentice" }, 26 },
            new object[] { new string[] { "Robin Hobb - Assassin Apprentice", "Isaac Asimov - Foundation", "Ayn Rand - FountainHead" }, 40 },
            new object[] { new string[] { "J.K Rowling - Goblet Of fire" }, 8 },
        };


        [Theory]
        [MemberData(nameof(Data))]
        public void TestBuy(string [] basketBook, double expected) 
        {
            this.Import();

            double price = TestStore.Buy(basketBook);

            Assert.Equal(expected, price);
        }


        [Fact]
        public void BuyError()
        {
            this.Import();

            Assert.Throws<NotEnoughInventoryException>(() => TestStore.Buy("mein kampf", "J.K Rowling - Goblet Of fire"));
        }


        [Fact]
        public void QuantityHarryPotter()
        {
            this.Import();

            Assert.Equal(2, TestStore.Quantity("J.K Rowling - Goblet Of fire"));
        }


        [Fact]
        public void QuantityError()
        {
            this.Import();

            Assert.Equal(0, TestStore.Quantity("mein kampf"));
        }

        [Fact]
        public void BuyAndRebuy()
        {
            this.Import();

            double price = TestStore.Buy("J.K Rowling - Goblet Of fire", "J.K Rowling - Goblet Of fire");

            Assert.Equal(16, price);

            Assert.Throws<NotEnoughInventoryException>(() => TestStore.Buy("J.K Rowling - Goblet Of fire"));
        }


    }
}
