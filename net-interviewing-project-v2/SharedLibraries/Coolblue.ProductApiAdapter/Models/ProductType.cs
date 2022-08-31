using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolblue.ProductApiAdapter.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool CanBeInsured { get; set; }
    }
}
