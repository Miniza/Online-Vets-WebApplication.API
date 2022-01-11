using Microsoft.EntityFrameworkCore;
using OnlineVetAPI.DataModels;

namespace OnlineVetAPI.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDBContext context;

        public AppRepository(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Owner>> GetOwnersAsync()
        {
            return await context.Owners.Include(b=>b.Pets).ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetPetsAsync()
        {
            return await context.Pets.Include(b=>b.Owner).ToListAsync();
        }
    }
}
