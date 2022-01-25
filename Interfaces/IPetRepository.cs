using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Interfaces
{
    public interface IPetRepository
    {
        public Task<IEnumerable<Pet>> GetPetsAsync();
        public Task<Pet> GetPetAsync(int Id);
        public Task<Pet> RemovePet(int Id);
        public Task<Pet> UpdatePet(int Id, Pet request);
        public Task<Pet> AddPet(Pet request);
        public Task<Boolean> Exists(int Id);

    }
}
