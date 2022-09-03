namespace RCMAppApi.Models
{
    public class Booking : BaseDomainEntity
    {
        public Booking()
        {

        }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
