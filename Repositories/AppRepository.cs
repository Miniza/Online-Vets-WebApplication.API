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

        public async Task<Pet> AddPet(Pet request)
        {
            var pet = await context.Pets.AddAsync(request);
            await context.SaveChangesAsync();
            return pet.Entity;
        }

        public async Task<bool> Exists(int Id)
        {
           return await context.Owners.AnyAsync(x => x.Id == Id);
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
                await context.SaveChangesAsync();
                return existingOwner;
            }
            return null;
        }

        public async Task<Pet> UpdatePet(int Id, Pet request)
        {
            var existingPet = await context.Pets.FindAsync(Id);
            if (existingPet != null)
            {
                existingPet.PetName = request.PetName;
                existingPet.PetType = request.PetType;
                existingPet.PetBreed = request.PetBreed;
                existingPet.DateOfBirth = request.DateOfBirth;
                await context.SaveChangesAsync();
                return existingPet;
            }
            return null; 
        }

        public async Task<bool> UpdateProfileImage(int Id, string ProfileImageUrl)
        {
            var Owner = await context.Owners.FindAsync(Id);

            if (Owner != null)
            {
                Owner.ProfileImageUrl = ProfileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
