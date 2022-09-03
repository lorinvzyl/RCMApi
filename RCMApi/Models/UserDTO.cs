namespace RCMAppApi.Models
{
    public class UserDTO : BaseDomainEntity
    {
        public UserDTO()
        {
        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Token { get; set; }
        public bool? IsNewsletter { get; set; }
        public string? HashedPassword { get; set; }
        public int? Iterations { get; set; }
        public int? MemoryLimit { get; set; }
    }
}
