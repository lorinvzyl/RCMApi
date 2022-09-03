namespace RCMAppApi.Models
{
    public class Comment : BaseDomainEntity
    {
        public Comment()
        {
            Replies = new HashSet<Comment>();
        }
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CommentId { get; set; }
        public Comment Parent { get; set; }
        public int? VideoId { get; set; }
        public Video? Video { get; set; }
        public int? BlogId { get; set; }
        public Blog? Blog { get; set; }
        public ICollection<Comment> Replies { get; set; }
    }
}
