using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int Id);
    }
}
