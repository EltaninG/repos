using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            new object[] { new string[] { "J.K Rowling - Goblet Of fire" , "Ayn Rand - FountainHead", "Ayn Rand - FountainHead", "Isaac Asimov - Foundation" }, 48 },
            new object[] { new string[] { "J.K Rowling - Goblet Of fire" , "Ayn Rand - FountainHead", "Ayn Rand - FountainHead", "Isaac Asimov - Foundation", "Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice", }, 70 },
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
            string missingBook = "mein kampf";
            string missingBook2 = "Minecraft";
            string availableBook = "J.K Rowling - Goblet Of fire";

            var exception = Assert.Throws<NotEnoughInventoryException>(() => TestStore.Buy(missingBook));
            Assert.Equal(1, exception.Missing.First().Quantity);
            Assert.Equal(missingBook, exception.Missing.First().Name);

            var exception1 = Assert.Throws<NotEnoughInventoryException>(() => TestStore.Buy(missingBook, availableBook));
            Assert.Equal(1, exception1.Missing.First().Quantity);
            Assert.Equal(missingBook, exception1.Missing.First().Name);

            var exception2 = Assert.Throws<NotEnoughInventoryException>(() => TestStore.Buy(missingBook, missingBook2));
            Assert.Equal(2, exception2.Missing.Count());
            Assert.Equal(1, exception2.Missing.First().Quantity);
            Assert.Equal(missingBook, exception2.Missing.First().Name);
            Assert.Equal(1, exception2.Missing.ElementAt(1).Quantity);
            Assert.Equal(missingBook2, exception2.Missing.ElementAt(1).Name);

            var exception3 = Assert.Throws<NotEnoughInventoryException>(() => TestStore.Buy(missingBook, missingBook));
            Assert.Equal(2, exception3.Missing.First().Quantity);
            Assert.Equal(missingBook, exception3.Missing.First().Name);
        }


        [Fact]
        public void Buy_NULL()
        {
            this.Import();
            var exception = Assert.Throws<ArgumentNullException>(() => TestStore.Buy(null));
            Assert.Equal("basketByNames", exception.ParamName);
        }


        [Fact]
        public void Test_Quantity()
        {
            this.Import();
            Assert.Equal(2, TestStore.Quantity("J.K Rowling - Goblet Of fire"));
        }

        [Fact]
        public void Buy_Should_Reduce_The_Quantity_After_A_Sale()
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
        public void Buy_Entire_Stock_And_Rebuy_Error()
        {
            this.Import();
            double price = TestStore.Buy("J.K Rowling - Goblet Of fire", "J.K Rowling - Goblet Of fire");
            Assert.Equal(16, price);
            Assert.Throws<NotEnoughInventoryException>(() => TestStore.Buy("J.K Rowling - Goblet Of fire"));
        }
    }
}
