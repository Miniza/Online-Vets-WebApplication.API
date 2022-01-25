using Microsoft.EntityFrameworkCore;
using OnlineVetAPI.DataModels;
using OnlineVetAPI.Interfaces;

namespace OnlineVetAPI.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDBContext context;

        public PetRepository(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<Pet> AddPet(Pet request)
        {
            var pet = await context.Pets.AddAsync(request);
            return pet.Entity;
        }

        public async Task<bool> Exists(int Id)
        {
            return await context.Owners.AnyAsync(x => x.Id == Id);
        }

        public async Task<Pet> GetPetAsync(int Id)
        {
            return await context.Pets.Include(b => b.Owner).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Pet>> GetPetsAsync()
        {
            return await context.Pets.Include(b => b.Owner).ToListAsync();
        }

        public async Task<Pet> RemovePet(int Id)
        {
            var pet = await context.Pets.Include(b => b.Owner).FirstOrDefaultAsync(x => x.Id == Id);
            context.Pets.Remove(pet);
            return pet;
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
                return existingPet;
            }
            return null;
        }

    }
}
