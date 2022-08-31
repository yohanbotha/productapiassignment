using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolblue.ProductApiAdapter.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SalesPrice { get; set; }
        public int ProductTypeId { get; set; }
    }
}
