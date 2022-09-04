using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Dtos.Rate
{
    public class RateDto
    {
        public int ProductTypeId { get; set; }
        public float SurchargeRate { get; set; }
    }
}
