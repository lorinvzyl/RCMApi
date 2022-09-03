namespace RCMAppApi.Models
{
    public class BookingDTO : BaseDomainEntity
    {
        public BookingDTO()
        {

        }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
    }
}
