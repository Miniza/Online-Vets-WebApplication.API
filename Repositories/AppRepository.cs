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

        public async Task<Owner> AddOwner(Owner request)
        {
            var owner = await context.Owners.AddAsync(request);
            await context.SaveChangesAsync();
            return owner.Entity;
        }

        public async Task<Owner> GetOwnerAsync(int Id)
        {
            return await context.Owners.Include(b => b.Pets).FirstOrDefaultAsync(x=>x.Id == Id);
        }

        public async Task<IEnumerable<Owner>> GetOwnersAsync()
        {
            return await context.Owners.Include(b=>b.Pets).ToListAsync();
        }

        public async Task<Pet> GetPetAsync(int Id)
        {
            return await context.Pets.Include(b => b.Owner).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Pet>> GetPetsAsync()
        {
            return await context.Pets.Include(b=>b.Owner).ToListAsync();
        }

        public async Task<Owner> RemoveOwner(int Id)
        {
            var owner = await context.Owners.Include(b => b.Pets).FirstOrDefaultAsync(x => x.Id == Id);
            context.Owners.Remove(owner);
            await context.SaveChangesAsync();
            return owner;

        }

        public async Task<Pet> RemovePet(int Id)
        {
            var pet = await context.Pets.Include(b => b.Owner).FirstOrDefaultAsync(x => x.Id == Id);
            context.Pets.Remove(pet);
            await context.SaveChangesAsync();
            return pet;
        }
    }
}
