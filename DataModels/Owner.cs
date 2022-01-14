using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVetAPI.DataModels
{
    public class Owner
    {
        
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string? FirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [Column]
        public string? MobileNumber { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? OwnerEmail { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Address { get; set; }
        [Column]
        public string? ProfileImageUrl { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
