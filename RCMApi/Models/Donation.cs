namespace RCMAppApi.Models
{
    public class Donation : BaseDomainEntity
    {
        public Donation()
        {

        }
        public string? Message { get; set; }
        public int? UserId { get; set; }
        public double? Amount { get; set; }
        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
