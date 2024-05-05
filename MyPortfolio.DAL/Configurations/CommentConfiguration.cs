using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AuthorName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CommentText).IsRequired();
            builder.Property(x => x.AuthorEmailAddress).IsRequired();
            builder.Property(x => x.Date).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsShared).HasDefaultValue(false);
            builder.Property(x => x.BlogId).IsRequired();
            builder.HasOne(x => x.Blog).WithMany(x => x.Comments).HasForeignKey(x => x.BlogId);
        }
    }
}
