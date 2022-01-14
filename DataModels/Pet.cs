using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVetAPI.DataModels
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? PetName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? PetType { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? PetBreed { get; set; }
        [Column]
        public DateTime DateOfBirth { get; set; }

        public  Owner Owner { get; set; }
    }
}
