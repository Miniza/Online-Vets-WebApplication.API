namespace OnlineVetAPI.DomainModels
{
    public class AddNewPet
    {
        public string? PetName { get; set; }
        public string? PetType { get; set; }
        public string? PetBreed { get; set; }
        public string? DateOfBirth { get; set; }

        public Owner Owner { get; set; }
    }
}
