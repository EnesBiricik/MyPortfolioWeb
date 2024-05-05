namespace MyPortfolio.Dtos
{
    public class SocialMediaListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public uint ClickCount { get; set; }

    }
}
