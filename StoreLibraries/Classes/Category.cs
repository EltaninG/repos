using Newtonsoft.Json;
using System;

namespace StoreLibraries.Classes
{
    public class Category
    {
        #region ° Properties °

        /// <summary>
        /// Name of the Category
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Discount
        /// </summary>
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
