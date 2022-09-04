using Insurance.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Data.EntityConfigurations
{
    public class RateEntityConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("rate");
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.ProductTypeId).HasColumnName("ProductTypeId");
            builder.Property(e => e.SurchargeRate);
        }
    }
}
