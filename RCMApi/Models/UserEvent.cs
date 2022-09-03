namespace RCMAppApi.Models
{
    public class UserEvent : BaseDomainEntity
    {
        public UserEvent()
        {

        }
        public int? UserId { get; set; }
        public int? EventId { get; set; }
        public bool? IsAttended { get; set; }
        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
