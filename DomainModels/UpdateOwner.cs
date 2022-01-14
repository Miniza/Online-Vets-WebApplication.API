﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVetAPI.DomainModels
{
    public class UpdateOwner
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNumber { get; set; }
        public string? OwnerEmail { get; set; }
        public string? Address { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
