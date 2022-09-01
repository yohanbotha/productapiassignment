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

        Task<Product> GetProductAsync(int productId);

        Task<IList<ProductType>> GetProductTypesAsync();

        Task<ProductType> GetProductTypeAsync(int productTypeId);
    }
}
