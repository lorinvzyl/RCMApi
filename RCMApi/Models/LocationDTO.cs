using RCMAppApi.Models;

namespace RCMApi.Models
{
    public class LocationDTO
    {
        public LocationDTO()
        {

        }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? MapsURL { get; set; }
        public string? Pastor { get; set; }
    }
}
