using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLibraries.Classes
{
    public class Stocktaking
    {
        #region ° Properties °

        public List<Book> Catalog { get; set; }

        public List<Category> Category { get; set; }

        #endregion



        public Stocktaking()
        {
            this.Catalog = new List<Book>();
            this.Category = new List<Category>();
        }


    }
}
