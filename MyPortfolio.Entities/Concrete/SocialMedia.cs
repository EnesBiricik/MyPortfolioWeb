namespace MyPortfolio.Entities.Concrete
{
    public class SocialMedia : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public uint ClickCount { get; set; }
    }
}
