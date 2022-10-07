using RCMApi.Models;

namespace RCMAppApi.Models
{
    public class Comment : BaseDomainEntity
    {
        public Comment()
        {
            Reply = new HashSet<Reply>();
        }
        public string? CommentText { get; set; }
        public int? UserId { get; set; }
        public int? VideoId { get; set; }
        public int? BlogId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Reply>? Reply { get; set; } = new List<Reply>();

        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
