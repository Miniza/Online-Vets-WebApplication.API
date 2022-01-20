using Microsoft.EntityFrameworkCore;
using OnlineVetAPI.DataModels;
using OnlineVetAPI.Interfaces;

namespace OnlineVetAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext context;

        public UserRepository(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<User> Create(User user)
        {
            await context.Users.AddAsync(user);
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetById(int Id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }
    }
}
