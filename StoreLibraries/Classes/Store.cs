using StoreLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using StoreLibraries.Exceptions;

namespace StoreLibraries.Classes
{
    public class Store : IStore
    {

        #region ° Properties °

        private Stocktaking Stock { get; set; }
        #endregion

        #region ° CTOR °

        public Store()
        {
        }
        #endregion

        #region ° Methods °

        /// <summary>
        /// Import Stock
        /// </summary>
        /// <param name="catalogAsJson"></param>
        public void Import(string catalogAsJson)
        {
            this.Stock = JsonConvert.DeserializeObject<Stocktaking>(catalogAsJson);
        }

        /// <summary>
        /// Return the book stock
        /// </summary>
        /// <param name="name">book's title</param>
        /// <returns></returns>
        public int Quantity(string name)
        {
            return (this.Stock?.Catalog?.Where(b => b.Name == name)?.Sum(b => b.Quantity)).GetValueOrDefault();
        }

        /// <summary>
        /// Verify if there are enough book in the stock
        /// Throw an exception if they are not enough book
        /// Destock the books to purchase
        /// return price
        /// </summary>
        /// <param name="basketByNames">List of book to purchase</param>
        /// <returns></returns>
        public double Buy(params string[] basketByNames)
        {
            
            List<Book> BasketBookList = this.Stock.CheckStock(basketByNames);
            return this.Stock.CalculatePriceAndDestock(BasketBookList);

        }
        #endregion
    }
}
