namespace MyPortfolio.Dtos
{
    public class SocialMediaUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set; }
        public string Link { get; set; }
    }
}
