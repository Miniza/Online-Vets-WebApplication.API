using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Repositories
{
    public interface IAppRepository 
    {
        public Task<IEnumerable<Owner>> GetOwnersAsync();
        public Task<IEnumerable<Pet>> GetPetsAsync();
    }
}
