using Insurance.Domain.Dtos.Product;

namespace Insurance.Domain.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductAsync(int productId);
    }
}
