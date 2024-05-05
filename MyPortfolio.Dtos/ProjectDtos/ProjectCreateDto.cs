namespace MyPortfolio.Dtos
{
    public class ProjectCreateDto : IDto
    {
        public string Name { get; set; }
        public string? CoverPhoto { get; set; }
        public string Description { get; set; }
        public string? LiveDemoLink { get; set; }
        public string GithubLink { get; set; }
    }
}
