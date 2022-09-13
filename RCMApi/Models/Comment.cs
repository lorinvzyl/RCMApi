namespace RCMAppApi.Models
{
    public class Comment : BaseDomainEntity
    {
        public Comment()
        {
            Reply = new HashSet<Comment>();
        }
        public string? CommentText { get; set; }
        public int? UserId { get; set; }
        public int? VideoId { get; set; }
        public int? BlogId { get; set; }
        public int? ParentId { get; set; }
        public int? CommentId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Comment>? Reply { get; set; } = new List<Comment>();

        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
