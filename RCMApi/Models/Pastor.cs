namespace RCMAppApi.Models
{
    public class Pastor : BaseDomainEntity
    {
        public Pastor()
        {
            Bookings = new HashSet<Booking>();
        }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
