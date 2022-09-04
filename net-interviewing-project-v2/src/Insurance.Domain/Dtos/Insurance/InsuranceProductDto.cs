using Insurance.Domain.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Dtos.Insurance
{
    public class InsuranceProductDto
    {
        public float InsuranceCost { get; set; }

        public ProductDto? Product { get; set; }
    }
}
