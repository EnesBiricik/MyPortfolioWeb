namespace MyPortfolio.Dtos
{
    public class PageSettingsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
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

    }
}
