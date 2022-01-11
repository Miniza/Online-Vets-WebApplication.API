using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Repositories
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

    }
}
