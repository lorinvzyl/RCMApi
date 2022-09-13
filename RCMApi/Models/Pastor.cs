using RCMAppApi.Models;

namespace RCMApi.Models
{
    public class Pastor : BaseDomainEntity
    {
        public Pastor()
        {

        }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
