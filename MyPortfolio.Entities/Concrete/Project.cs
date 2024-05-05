namespace MyPortfolio.Entities.Concrete
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string CoverPhoto { get; set; }
        public string Description { get; set; }
        public string LiveDemoLink { get; set; }
        public string GithubLink { get; set; }
        public DateTime Date { get; set; }
    }
}
