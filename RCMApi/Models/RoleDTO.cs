namespace RCMAppApi.Models
{
    public class RoleDTO : BaseDomainEntity
    {
        public RoleDTO()
        {
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
