using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.DAL.Configurations
{
    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(x => x.VisitorCount).HasDefaultValue(0);
        }
    }
}
