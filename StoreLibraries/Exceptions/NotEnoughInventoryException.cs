using StoreLibraries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreLibraries.Exceptions
{
    public class NotEnoughInventoryException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<INameQuantity> Missing { get; }

        public NotEnoughInventoryException(IEnumerable<INameQuantity> missing):base()
        {
            Missing = missing;
        }

    }
}
