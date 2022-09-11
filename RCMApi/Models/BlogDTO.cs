using System.ComponentModel.DataAnnotations.Schema;

namespace RCMAppApi.Models
{
    public class BlogDTO
    {
        public BlogDTO()
        {
        }
        public string? BlogTitle { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
    }
}
