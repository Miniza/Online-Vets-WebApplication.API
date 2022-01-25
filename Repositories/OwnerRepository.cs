using Microsoft.EntityFrameworkCore;
using OnlineVetAPI.DataModels;
using OnlineVetAPI.Interfaces;

namespace OnlineVetAPI.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDBContext context;

        public OwnerRepository(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<Owner> AddOwner(Owner request)
        {
            var owner = await context.Owners.AddAsync(request);
            return owner.Entity;
        }

        public async Task<bool> Exists(int Id)
        {
            return await context.Owners.AnyAsync(x => x.Id == Id);
        }

        public async Task<Owner> GetOwnerAsync(int Id)
        {
            return await context.Owners.Include(b => b.Pets).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Owner>> GetOwnersAsync()
        {
            return await context.Owners.Include(b => b.Pets).ToListAsync();
        }

        public async Task<Owner> RemoveOwner(int Id)
        {
            var owner = await context.Owners.Include(b => b.Pets).FirstOrDefaultAsync(x => x.Id == Id);
            context.Owners.Remove(owner);
            return owner;
        }

        public async Task<Owner> UpdateOwner(int Id, Owner request)
        {
            var existingOwner = await context.Owners.FindAsync(Id);
            if (existingOwner != null)
            {
                existingOwner.FirstName = request.FirstName;
                existingOwner.LastName = request.LastName;
                existingOwner.MobileNumber = request.MobileNumber;
                existingOwner.OwnerEmail = request.OwnerEmail;
                existingOwner.Address = request.Address;
                return existingOwner;
            }
            return null;
        }

        public async Task<bool> UpdateProfileImage(int Id, string ProfileImageUrl)
        {
            var Owner = await context.Owners.FindAsync(Id);

            if (Owner != null)
            {
                Owner.ProfileImageUrl = ProfileImageUrl;  
                return true;
            }
            return false;
        }
    }
}
