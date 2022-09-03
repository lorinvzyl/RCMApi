namespace RCMAppApi.Models
{
    public class Role : BaseDomainEntity
    {
        public Role()
        {
            Guid = Guid.NewGuid();
            UserRoles = new HashSet<UserRole>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
