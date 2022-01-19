using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext context;

        public UserRepository(AppDBContext context)
        {
            this.context = context;
        }
        public User Create(User user)
        {
            context.Users.Add(user);
            user.Id = context.SaveChanges();
            return user;
        }

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int Id)
        {
            return context.Users.FirstOrDefault(u => u.Id == Id);
        }
    }
}
