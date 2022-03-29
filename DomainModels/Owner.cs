using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVetAPI.DomainModels
{
    public class Owner
    {
        [Key]
        public int Id { get; private set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? FirstName { get; private set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; private set; }
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
