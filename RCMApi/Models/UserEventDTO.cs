namespace RCMAppApi.Models
{
    public class UserEventDTO : BaseDomainEntity
    {
        public UserEventDTO()
        {

        }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? EventId { get; set; }
        public Event? Event { get; set; }
        public bool? IsAttended { get; set; }
    }
}
