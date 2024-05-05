using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AuthorName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CommentText).IsRequired();
            builder.Property(x => x.Date).HasDefaultValueSql("getdate()");
            builder.Property(x => x.AuthorEmailAddress).IsRequired();
            builder.Property(x => x.IsShared).HasDefaultValue(false);
            builder.HasOne(x => x.Comment).WithMany(x => x.Replies).HasForeignKey(x => x.CommentId);
        }
    }
}
