using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Icon).IsRequired();
            builder.Property(x => x.Link).IsRequired();
            builder.Property(x => x.ClickCount).HasDefaultValue((uint)0);
        }
    }
}
