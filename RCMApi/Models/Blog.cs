using System.ComponentModel.DataAnnotations.Schema;

namespace RCMAppApi.Models
{
    public class Blog : BaseDomainEntity
    {
        public Blog()
        {
            BlogComments = new HashSet<Comment>();
        }
        public string BlogTitle { get; set; }
        [Column(TypeName = "text")]
        public string Content { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> BlogComments { get; set; }
    }
}
