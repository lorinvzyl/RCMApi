namespace RCMAppApi.Models
{
    public class User : BaseDomainEntity
    {
        public User()
        {

        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsNewsletter { get; set; }
        public byte[]? HashedPassword { get; set; }
        public int? Iterations { get; set; }
        public int? MemoryLimit { get; set; }
        public string? ProfilePicURL { get; set; }

        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
