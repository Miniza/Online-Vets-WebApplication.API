using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
       
        
        // PUT: api/Owners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] 
        public async Task<IActionResult> PutOwner(int id, [FromForm] UpdateOwner request)
        {
            if (await appRepository.Exists(id))
            {
               var updatedOwner = await appRepository.UpdateOwner(id, mapper.Map<DataModels.Owner>(request));
                if (updatedOwner != null)
                {
                    return Ok(mapper.Map<Owner>(updatedOwner));
                }
            }
                return NotFound();
        } 
        
        // POST: api/Owners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner([FromForm] AddNewOwner  request)
        {
            var newOwner = await appRepository.AddOwner(mapper.Map<DataModels.Owner>(request));
            return Ok(mapper.Map<Owner>(newOwner));
        } 
       

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
