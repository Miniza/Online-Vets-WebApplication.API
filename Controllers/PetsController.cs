using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineVetAPI.DomainModels;
using OnlineVetAPI.Repositories;

namespace OnlineVetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly IMapper mapper;

        public PetsController(IAppRepository appRepository, IMapper mapper)
        {
            this.appRepository = appRepository;
            this.mapper = mapper;
        }
       

        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPet()
        {
            var pets = await appRepository.GetPetsAsync();
            return Ok(mapper.Map<IEnumerable<Pet>>(pets));
            
        }
        
        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = await appRepository.GetPetAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Pet>(pet));
        }
        /*
        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pet pet)
        {
            if (id != pet.Id)
            {
                return BadRequest();
            }

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {
            _context.Pet.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPet", new { id = pet.Id }, pet);
        }
        */
        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await appRepository.RemovePet(id);
            if (pet == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
