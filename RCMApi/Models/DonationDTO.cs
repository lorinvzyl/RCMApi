namespace RCMAppApi.Models
{
    public class DonationDTO : BaseDomainEntity
    {
        public DonationDTO()
        {

        }
        public string? Message { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
