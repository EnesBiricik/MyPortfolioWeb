namespace MyPortfolio.Dtos
{
    public class ContactCreateDto : IDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
