namespace RCMAppApi.Models
{
    public class Role : BaseDomainEntity
    {
        public Role()
        {
        }
        public string? Name { get; set; }
        public string? Description { get; set; }

        /*DTO Model integration
        public string? Secret { get; set; }*/
    }
}
