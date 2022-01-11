using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineVetAPI.DomainModels;
using OnlineVetAPI.Repositories;

namespace OnlineVetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly IMapper mapper;

        public OwnersController(IAppRepository appRepository, IMapper mapper)
        {
            this.appRepository = appRepository;
            this.mapper = mapper;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwner()
        {
            var owners =  await appRepository.GetOwnersAsync();
            return Ok(mapper.Map<IEnumerable<Owner>>(owners));
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int Id)
        {
            var owner = await appRepository.GetOwnerAsync(Id);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Owner>(owner)); 
        }
        /*

        // PUT: api/Owners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(Guid id, Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
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

        // POST: api/Owners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            _context.Owner.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new { id = owner.Id }, owner);
        } */

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await appRepository.RemoveOwner(id);
            if (owner == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
