using RCMAppApi.Models;

namespace RCMApi.Models
{
    public class ReplyDTO
    {
        public int? Id { get; set; }
        public string CommentText { get; set; }
        public int? CommentId { get; set; }
        public string UserName { get; set; }

        public virtual User? User { get; set; }
    }
}
