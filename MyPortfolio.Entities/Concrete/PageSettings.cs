namespace MyPortfolio.Entities.Concrete
{
    public class PageSettings : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfilePhoto { get; set; }
        public string SloganSentence { get; set; }
        public string TitleIcon { get; set; }
        public string CommentAuthorProfilePhoto { get; set; }
        public string NavbarLogoImage { get; set; }
        public string FooterLogoImage { get; set; }
        public string AboutDescription { get; set; }
        public string Job { get; set; }
        public string ShortDescription { get; set; }
        public uint VisitorCount { get; set; }

    }
}
