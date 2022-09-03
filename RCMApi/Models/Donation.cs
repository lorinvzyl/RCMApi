namespace RCMAppApi.Models
{
    public class Donation : BaseDomainEntity
    {
        public Donation()
        {

        }
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
