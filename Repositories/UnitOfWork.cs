using OnlineVetAPI.DataModels;
using OnlineVetAPI.Interfaces;

namespace OnlineVetAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext context;

        public UnitOfWork(AppDBContext context)
        {
            this.context = context;
        }
        public IAppRepository AppRepository => new AppRepository(context);

        public IUserRepository UserRepository => new UserRepository(context);

        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
