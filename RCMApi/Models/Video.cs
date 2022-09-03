namespace RCMAppApi.Models
{
    public class Video : BaseDomainEntity
    {
        public Video()
        {
            VideoComments = new HashSet<Comment>();
        }
        public string VideoTitle { get; set; }
        public string VideoDescription { get; set; }
        public string VideoURL { get; set; }
        public ICollection<Comment> VideoComments { get; set; }
    }
}
