using Library.ProductApiAdapter.Models;

namespace Library.ProductApiAdapter
{
    public interface IProductDataApiClient
    {
        Task<IList<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(int productId);

        Task<IList<ProductType>> GetProductTypesAsync();

        Task<ProductType> GetProductTypeAsync(int productTypeId);
    }
}
