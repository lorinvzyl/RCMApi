namespace RCMApi.Models
{
    public class ReplyDTO
    {
        public int? Id { get; set; }
        public string CommentText { get; set; }
        public int? ParentId { get; set; }
        public string UserName { get; set; }
    }
}
