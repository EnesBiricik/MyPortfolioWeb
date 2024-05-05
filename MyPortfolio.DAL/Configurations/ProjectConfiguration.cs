using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CoverPhoto).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.LiveDemoLink).IsRequired();
            builder.Property(x => x.GithubLink).IsRequired();
            builder.Property(x => x.Date).HasDefaultValueSql("getdate()");
        }
    }
}
