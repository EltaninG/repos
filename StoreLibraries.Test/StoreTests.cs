using System.Collections.Generic;
using System.IO;
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

            using (StreamReader r = new StreamReader("data.json"))
            {
                string ImportJson = r.ReadToEnd();
                TestStore = new Store();
                TestStore.Import(ImportJson);
            }
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
        public void TestBuy(string[] basketBook, double expected)
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
        public void QuantityHarryPotter2()
        {
            this.Import();
            double price = TestStore.Buy("J.K Rowling - Goblet Of fire");
            Assert.Equal(1, TestStore.Quantity("J.K Rowling - Goblet Of fire"));
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
