namespace MyPortfolio.Dtos
{
    public class ContactListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

    }
}
