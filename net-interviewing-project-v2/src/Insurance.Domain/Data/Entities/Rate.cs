using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Data.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public float SurchargeRate { get; set; }
    }
}
