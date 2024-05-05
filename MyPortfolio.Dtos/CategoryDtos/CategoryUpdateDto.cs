namespace MyPortfolio.Dtos
{
    public class CategoryUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
