﻿namespace RCMApi.Models
{
    //This class takes values from the app, to allow for deserializing of string.
    public class UserString
    {
        public UserString()
        {

        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsNewsletter { get; set; }
        public string? HashedPassword { get; set; }
    }
}