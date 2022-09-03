namespace RCMAppApi.Models
{
    public class User : BaseDomainEntity
    {
        public User()
        {
            UserEvents = new HashSet<UserEvent>();
            Comments = new HashSet<Comment>();
            Donations = new HashSet<Donation>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Token { get; set; }
        public bool IsNewsletter { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
