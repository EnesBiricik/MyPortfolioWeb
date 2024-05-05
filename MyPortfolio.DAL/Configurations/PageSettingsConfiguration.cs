using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Configurations
{
    public class PageSettingsConfiguration : IEntityTypeConfiguration<PageSettings>
    {
        public async void Configure(EntityTypeBuilder<PageSettings> builder)
        {
            builder.HasData(new PageSettings
            {
                Id = 1,
                AboutDescription = "Lorem Ipsum Dolor Sit",
                Email = "enesbiricikcom@gmail.com",
                Name = "Enes Biricik",
                SloganSentence = "Lorem Ipsum!",
                ProfilePhoto = "profile.png",
                CommentAuthorProfilePhoto = "CommentAuthorProfilePhoto.png",
                NavbarLogoImage = "IconNavbar.png",
                FooterLogoImage = "IconNavbar.png",
                TitleIcon = "TitleIcon.png",
                Job = "Software Developer",
                ShortDescription = "Lorem Ipsum!"
            });


            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.ProfilePhoto).IsRequired().HasDefaultValue("profile.png");
            builder.Property(x => x.SloganSentence).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CommentAuthorProfilePhoto).IsRequired().HasDefaultValue("CommentAuthorProfilePhoto.png");
            builder.Property(x => x.NavbarLogoImage).IsRequired().HasDefaultValue("IconNavbar.png");
            builder.Property(x => x.FooterLogoImage).IsRequired().HasDefaultValue("IconNavbar.png");
            builder.Property(x => x.AboutDescription).IsRequired();
            builder.Property(x => x.TitleIcon).IsRequired().HasDefaultValue("TitleIcon.png");
            builder.Property(x => x.Job).IsRequired();
            builder.Property(x => x.VisitorCount).HasDefaultValue(0);
            builder.Property(x => x.ShortDescription).IsRequired();
        }
    }
}
