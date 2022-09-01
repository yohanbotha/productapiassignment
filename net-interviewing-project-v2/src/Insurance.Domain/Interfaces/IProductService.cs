using Insurance.Domain.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain
{
    public interface IProductService
    {
        Task<ProductDto> GetProductAsync(int productId);
    }
}
