namespace RCMAppApi.Models
{
    public class Newsletter : BaseDomainEntity
    {
        public Newsletter()
        {

        }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
