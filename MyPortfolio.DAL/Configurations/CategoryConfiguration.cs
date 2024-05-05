using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = 1,
                Name = "Yazılım",

            });
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasMany(x => x.Blogs).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
