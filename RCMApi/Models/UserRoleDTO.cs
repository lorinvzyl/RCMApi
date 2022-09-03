namespace RCMAppApi.Models
{
    public class UserRoleDTO : BaseDomainEntity
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
