﻿namespace RCMAppApi.Models
{
    public class UserRole : BaseDomainEntity
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        //DTO Model integration
        public string? Secret { get; set; }
    }
}
