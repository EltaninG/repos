using StoreLibraries.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreLibraries.Classes
{
    public class Stocktaking
    {
        #region ° Properties °

        public List<Book> Catalog { get; }

        public List<Category> Category { get; }

        #endregion

        #region ° CTOR °

        public Stocktaking()
        {
            this.Catalog = new List<Book>();
            this.Category = new List<Category>();
        }

        #endregion


        #region ° Methods °

        
        internal double CalculatePriceAndDestock(List<Book> basketBookList)
        {
            var enumCategory = basketBookList.GroupBy(b => b.Category)
                                             .Select(group => new {
                                                    Name = group.Key,
                                                    Count = group.Count()
                                                });

            double price = 0;
            foreach (var currentbook in basketBookList)
            {
                
                if (enumCategory.FirstOrDefault(c=>c.Name == currentbook.Category).Count > 1)
                {
                    price += currentbook.Price * (1 - (this.Category.First(c => c.Name == currentbook.Category).Discount)) + (currentbook.Quantity - 1) * currentbook.Price;
                }
                else
                {
                    price += currentbook.Price * currentbook.Quantity;
                }
                this.Catalog.First(b => b.Name == currentbook.Name).Remove(currentbook.Quantity);
            }

            return price;
        }


        internal List<Book> CheckStock(params string[] basketByNames)
        {
            var EnumBook = basketByNames.GroupBy(b => b);
            List<Book> missingBookList = new List<Book>();
            List<Book> basketBookList = new List<Book>();

            foreach (var cb in EnumBook)
            {
                Book currentBookCatalog = this.Catalog.FirstOrDefault(b => b.Name == cb.Key);
                if (currentBookCatalog == null)
                {
                    missingBookList.Add(new Book(cb.Key, cb.Count()));
                }
                else
                {
                    if (currentBookCatalog.Quantity < cb.Count())
                    {
                        missingBookList.Add(new Book(cb.Key, cb.Count() - currentBookCatalog.Quantity));
                    }
                    else
                    {
                        basketBookList.Add(currentBookCatalog.NewBasketBook(cb.Count()));
                    }
                }
            }

            if (missingBookList.Count() > 0)
            {
                throw new NotEnoughInventoryException(missingBookList);
            }

            return basketBookList;
        }

        #endregion
    }
}
