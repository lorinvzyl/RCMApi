using System.ComponentModel.DataAnnotations.Schema;

namespace RCMAppApi.Models
{
    public class Blog : BaseDomainEntity
    {
        public Blog()
        {
        }
        public string? BlogTitle { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int? UserId { get; set; }

        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
