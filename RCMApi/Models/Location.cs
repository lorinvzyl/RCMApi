using RCMAppApi.Models;

namespace RCMApi.Models
{
    public class Location : BaseDomainEntity
    {
        public Location()
        {

        }

        public string? Name { get; set; }
        public string? MapsURL { get; set; }
        public int? PastorId { get; set; }
    }
}
