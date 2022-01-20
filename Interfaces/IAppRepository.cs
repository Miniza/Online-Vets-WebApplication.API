using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Interfaces
{
    public interface IAppRepository 
    {
        public Task<IEnumerable<Owner>> GetOwnersAsync();
        public Task<Owner> GetOwnerAsync(int Id);
        public Task<IEnumerable<Pet>> GetPetsAsync();
        public Task<Pet> GetPetAsync(int Id);
        public Task<Owner> AddOwner(Owner request);
        public Task<Owner> RemoveOwner(int Id);
        public Task<Pet> RemovePet(int Id);
        public Task<Boolean> Exists(int Id);
        public Task<Owner> UpdateOwner(int Id, Owner request);
        public Task<Pet> UpdatePet(int Id, Pet request);
        public Task<Pet> AddPet(Pet request);
        public Task<bool> UpdateProfileImage(int Id, string ProfileImageUrl);

    }
}
