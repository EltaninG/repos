using StoreLibraries.Interfaces;
using System;
using System.Collections.Generic;

namespace StoreLibraries.Exceptions
{
    public class NotEnoughInventoryException : Exception
    {
        public IEnumerable<INameQuantity> Missing { get; }

        public NotEnoughInventoryException(IEnumerable<INameQuantity> missing):base()
        {
            Missing = missing;
        }

    }
}
