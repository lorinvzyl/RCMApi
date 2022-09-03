namespace RCMAppApi.Models
{
    public class CommentDTO : BaseDomainEntity
    {
        public CommentDTO()
        {
        }
        public string? CommentText { get; set; }
        public int? UserId { get; set; }
        public int? CommentId { get; set; }
        public int? VideoId { get; set; }
        public int? BlogId { get; set; }
    }
}
