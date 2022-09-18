using RCMApi.Models;

namespace RCMAppApi.Models
{
    public class CommentDTO
    {
        public CommentDTO()
        {
            Reply = new HashSet<ReplyDTO>();
        }
        public int Id { get; set; }
        public string? CommentText { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public int? BlogId { get; set; }
        public virtual ICollection<ReplyDTO>? Reply { get; set; } = new List<ReplyDTO>();
    }
}
