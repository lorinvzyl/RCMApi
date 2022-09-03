namespace RCMAppApi.Models
{
    public class UserEventDTO : BaseDomainEntity
    {
        public UserEventDTO()
        {

        }
        public bool? IsAttended { get; set; }
    }
}
