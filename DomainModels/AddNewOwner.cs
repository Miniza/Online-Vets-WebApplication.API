﻿namespace OnlineVetAPI.DomainModels
{
    public class AddNewOwner
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long MobileNumber { get; set; }
        public string? OwnerEmail { get; set; }
        public string? Address { get; set; }
    }
}
