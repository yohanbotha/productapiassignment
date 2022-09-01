using Insurance.Domain.Dtos.Product;

namespace Insurance.Domain
{
    public interface IProductService
    {
        Task<ProductDto> GetProductAsync(int productId);
    }
}
