using Coolblue.ProductApiAdapter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolblue.ProductApiAdapter
{
    public interface IProductDataApiClient
    {
        Task<IList<Product>> GetProductsAsync();
    }
}
