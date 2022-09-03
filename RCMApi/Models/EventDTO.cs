﻿namespace RCMAppApi.Models
{
    public class EventDTO : BaseDomainEntity
    {
        public EventDTO()
        {
        }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
        public int? SpacesAvailable { get; set; }
        public int? SpacesTaken { get; set; }
        public string? Venue { get; set; }
        public DateTime? RSVPCloseDate { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
