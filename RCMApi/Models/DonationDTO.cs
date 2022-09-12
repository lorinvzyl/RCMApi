namespace RCMAppApi.Models
{
    public class DonationDTO
    {
        public DonationDTO()
        {

        }
        public string? Message { get; set; }
        public string? UserEmail { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
