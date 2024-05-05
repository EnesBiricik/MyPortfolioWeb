namespace MyPortfolio.Entities.Concrete
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

    }
}
