namespace RCMAppApi.Models
{
    public class BaseDomainEntity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
