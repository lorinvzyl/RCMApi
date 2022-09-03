namespace RCMAppApi.Models
{
    public class CommentDTO : BaseDomainEntity
    {
        public CommentDTO()
        {
        }
        public string? CommentText { get; set; }
    }
}
