namespace RCMAppApi.Models
{
    public class UserEventDTO
    {
        public UserEventDTO()
        {

        }
        public string? UserEmail { get; set; }
        public int? EventId { get; set; }
        public bool? IsAttended { get; set; }
    }
}
