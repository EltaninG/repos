using Newtonsoft.Json;
using System;

namespace StoreLibraries.Classes
{
    public class Category
    {
        #region ° Properties °

        public String Name { get; }

        public double Discount { get; }

        #endregion

        #region ° CTOR °

        [JsonConstructor]
        public Category(String Name, double Discount)
        {
            this.Name = Name;
            this.Discount = Discount;
        }

        #endregion

    }
}
