using StoreLibraries.Interfaces;
using System;

namespace StoreLibraries.Classes
{
    public class Book : INameQuantity
    {
        #region ° Properties °

        public String Name { get; set; }

        public String Category { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        #endregion

        #region ° CTOR °

        public Book()
        {

        }

        public Book(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }

        public Book ShallowCopy()
        {
            return (Book)this.MemberwiseClone();
        }

        #endregion

    }
}
