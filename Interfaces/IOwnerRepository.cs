using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Interfaces
{
    public interface IOwnerRepository
    {
        public Task<IEnumerable<Owner>> GetOwnersAsync();
        public Task<Owner> GetOwnerAsync(int Id);
        public Task<Owner> AddOwner(Owner request);
        public Task<Owner> RemoveOwner(int Id);
        public Task<Owner> UpdateOwner(int Id, Owner request);
        public Task<Boolean> Exists(int Id);
        public Task<bool> UpdateProfileImage(int Id, string ProfileImageUrl);
    }
}
