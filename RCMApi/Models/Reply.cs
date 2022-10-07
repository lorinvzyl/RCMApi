using RCMAppApi.Models;

namespace RCMApi.Models
{
    public class Reply : BaseDomainEntity
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
