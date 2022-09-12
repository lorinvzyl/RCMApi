namespace RCMAppApi.Models
{
    public class Comment : BaseDomainEntity
    {
        public Comment()
        {
        }
        public string? CommentText { get; set; }
        public int? UserId { get; set; }
        public int? VideoId { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }

        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
