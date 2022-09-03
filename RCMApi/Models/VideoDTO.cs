namespace RCMAppApi.Models
{
    public class VideoDTO : BaseDomainEntity
    {
        public VideoDTO()
        {
            VideoComments = new HashSet<Comment>();
        }
        public string? VideoTitle { get; set; }
        public string? VideoDescription { get; set; }
        public string? VideoURL { get; set; }
        public ICollection<Comment>? VideoComments { get; set; }
    }
}
