using StoreLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using StoreLibraries.Exceptions;

namespace StoreLibraries.Classes
{
    public class Store : IStore
    {

        #region ° Properties °

        public Stocktaking Stock { get; set; }
        #endregion

        #region ° CTOR °

        public Store()
        {
        }
        #endregion

        #region ° Methods °

        
        public void Import(string catalogAsJson)
        {
            this.Stock = JsonSerializer.Deserialize<Stocktaking>(catalogAsJson);
        }

        /// <summary>
        /// Return 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Quantity(string name)
        {
            return (this.Stock.Catalog.Where(b => b.Name == name)?.Sum(b => b.Quantity)).GetValueOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basketByNames"></param>
        /// <returns></returns>
        public double Buy(params string[] basketByNames)
        {
            var EnumBook = basketByNames.GroupBy(b => b);
            Dictionary<string, int> dictionaryCategory = new Dictionary<string, int>();
            List<Book> MissingBookList = new List<Book>();
            List<Book> BasketBookList = new List<Book>();

            foreach (var cb in EnumBook)
            {
                Book currentBookCatalog = this.Stock.Catalog.FirstOrDefault(b => b.Name == cb.Key);
                if (currentBookCatalog == null)
                {
                    MissingBookList.Add(new Book(cb.Key, cb.Count()));
                }
                else
                {
                    if (currentBookCatalog.Quantity < cb.Count())
                    {
                        MissingBookList.Add(new Book(cb.Key, cb.Count() - currentBookCatalog.Quantity));
                    }
                    else
                    {
                        Book currentBasketBook = currentBookCatalog.ShallowCopy();
                        currentBasketBook.Quantity = cb.Count();
                        BasketBookList.Add(currentBasketBook);
                        Category bookCategory = this.Stock.Category.FirstOrDefault(c => c.Name == currentBookCatalog.Category);
                        if (dictionaryCategory.ContainsKey(currentBookCatalog.Category))
                        {
                            dictionaryCategory[currentBookCatalog.Category]++;
                        }
                        else
                        {
                            dictionaryCategory.Add(currentBookCatalog.Category, 1);
                        }
                    }
                }
            }

            if (MissingBookList.Count() > 0)
            {
                throw new NotEnoughInventoryException(MissingBookList);
            }

            double price = 0;
            foreach (var currentbook in BasketBookList)
            {
                //Reduction des stocks
                this.Stock.Catalog.First(b => b.Name == currentbook.Name).Quantity -= currentbook.Quantity;
                if (dictionaryCategory[currentbook.Category] > 1)
                {
                    price += currentbook.Price * (1 - (this.Stock.Category.First(c => c.Name == currentbook.Category).Discount)) + (currentbook.Quantity - 1) * currentbook.Price;
                }
                else
                {
                    price += currentbook.Price * currentbook.Quantity;
                }
            }

            return price;

        }
        #endregion
    }
}
