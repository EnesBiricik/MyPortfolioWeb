using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.EmailAddress).IsRequired();
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.Date).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsRead).HasDefaultValue(false);
        }
    }
}
