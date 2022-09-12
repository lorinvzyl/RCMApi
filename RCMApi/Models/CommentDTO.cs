namespace RCMAppApi.Models
{
    public class CommentDTO
    {
        public CommentDTO()
        {
        }
        public string? CommentText { get; set; }
        public string? UserEmail { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }
        public int? ParentId { get; set; }
    }
}
