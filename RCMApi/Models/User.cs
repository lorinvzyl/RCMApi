namespace RCMAppApi.Models
{
    public class User : BaseDomainEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            UserEvents = new HashSet<UserEvent>();
            Comments = new HashSet<Comment>();
            Bookings = new HashSet<Booking>();
            Donations = new HashSet<Donation>();
        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Token { get; set; }
        public bool? IsNewsletter { get; set; }
        public string? HashedPassword { get; set; }
        public int? Iterations { get; set; }
        public int? MemoryLimit { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }

        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
