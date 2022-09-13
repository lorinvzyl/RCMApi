namespace RCMAppApi.Models
{
    public class CommentDTO
    {
        public CommentDTO()
        {
            Reply = new HashSet<CommentDTO>();
        }
        public int Id { get; set; }
        public string? CommentText { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public int? BlogId { get; set; }
        public int? ParentId { get; set; }
        public int? CommentId { get; set; }
        public virtual ICollection<CommentDTO>? Reply { get; set; } = new List<CommentDTO>();
    }
}
