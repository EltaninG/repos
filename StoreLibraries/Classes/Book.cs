using StoreLibraries.Interfaces;
using System;
using Newtonsoft.Json;

namespace StoreLibraries.Classes
{
    public class Book : INameQuantity
    {
        #region ° Properties °
        public String Name { get; }
        public String Category { get;}
        public double Price { get; }
        public int Quantity { get; private set; }

        #endregion

        #region ° CTOR °


        [JsonConstructor]
        public Book(String Name, String Category, double Price, int Quantity)
        {
            this.Name = Name;
            this.Category = Category;
            this.Price = Price;
            this.Quantity = Quantity;
        }


        public Book(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }
        #endregion

        public Book NewBasketBook(int quantity)
        {
            Book BasketBook = new Book(this.Name, this.Category, this.Price, quantity);
            return BasketBook;
        }

        public void Remove(int quantity)
        {
            this.Quantity -= quantity;
        }

    }
}
