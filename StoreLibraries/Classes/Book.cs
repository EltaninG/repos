using StoreLibraries.Interfaces;
using System;
using Newtonsoft.Json;

namespace StoreLibraries.Classes
{
    public class Book : INameQuantity
    {
        #region ° Properties °

        /// <summary>
        /// Book title
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Book category
        /// </summary>
        public String Category { get;}

        /// <summary>
        /// Book price
        /// </summary>
        public double Price { get; }

        /// <summary>
        /// quantity of book
        /// </summary>
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

        /// <summary>
        /// Create a new book for the basket
        /// </summary>
        /// <param name="quantity">Quantity desired</param>
        /// <returns>return a book with all informations</returns>
        public Book NewBasketBook(int quantity)
        {
            Book BasketBook = new Book(this.Name, this.Category, this.Price, quantity);
            return BasketBook;
        }

        /// <summary>
        /// Remove the quantity of book
        /// </summary>
        /// <param name="quantity">quanity to remove</param>
        public void Remove(int quantity)
        {
            this.Quantity -= quantity;
        }

    }
}
