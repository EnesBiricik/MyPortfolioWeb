using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CoverPhoto).IsRequired().HasDefaultValue("blogPostPhoto.jpg");
            builder.Property(x => x.Date).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsFeatured).HasDefaultValue(false);
            builder.Property(x => x.ReadingTime).IsRequired().HasDefaultValueSql("0");
        }
    }
}
