using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLibraries.Interfaces
{
    public interface IStore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalogAsJson"></param>
        void Import(string catalogAsJson);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int Quantity(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basketByNames"></param>
        /// <returns></returns>
        double Buy(params string[] basketByNames);

    }
}
