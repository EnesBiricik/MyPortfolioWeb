using AutoMapper;
using MyPortfolio.BAL.Mappings.AutoMapper;

namespace MyPortfolio.BAL.Helpers
{
    public static class ProfileHelper
    {
        public static List<Profile> GetProfiles()
        {
            return new List<Profile>
            {
                new BlogProfile(),
                new CategoryProfile(),
                new CommentProfile(),
                new ContactProfile(),
                new PageSettingsProfile(),
                new ProjectProfile(),
                new ReplyProfile(),
                new SocialMediaProfile()
            };
        }
    }
}
