using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Create(User user);
        public Task<User> GetByEmail(string email);
        public Task<User> GetById(int Id);
    }
}
