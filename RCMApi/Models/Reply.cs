using RCMAppApi.Models;

namespace RCMApi.Models
{
    public class Reply : BaseDomainEntity
    {
        public int ParentId { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }
    }
}
