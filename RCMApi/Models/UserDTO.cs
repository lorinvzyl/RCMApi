namespace RCMAppApi.Models
{
    public class UserDTO
    {
        public UserDTO()
        {
        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsNewsletter { get; set; }
        public string? Password { get; set; }
    }
}
