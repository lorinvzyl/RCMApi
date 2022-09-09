namespace RCMAppApi.Models
{
    public class Video : BaseDomainEntity
    {
        public Video()
        {
        }
        public string? VideoTitle { get; set; }
        public string? VideoDescription { get; set; }
        public string? VideoURL { get; set; }
        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
