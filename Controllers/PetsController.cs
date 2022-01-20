using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineVetAPI.DomainModels;
using OnlineVetAPI.Interfaces;

namespace OnlineVetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PetsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
       

        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPet()
        {
            var pets = await unitOfWork.AppRepository.GetPetsAsync();
            await unitOfWork.SaveAsync();
            return Ok(mapper.Map<IEnumerable<Pet>>(pets));
            
        }
        
        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = await unitOfWork.AppRepository.GetPetAsync(id);
            await unitOfWork.SaveAsync();

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Pet>(pet));
        }
        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, [FromForm] UpdatePet request)
        {
            if (await unitOfWork.AppRepository.Exists(id))
            {
                var updatedPet = await unitOfWork.AppRepository.UpdatePet(id, mapper.Map<DataModels.Pet>(request));
                await unitOfWork.SaveAsync();
                if (updatedPet != null)
                {
                    return Ok(mapper.Map<Pet>(updatedPet));
                }
                
            }
            return NotFound();
        }
        
        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet([FromForm
            ] AddNewPet request)
        {
            var newPet = await unitOfWork.AppRepository.AddPet(mapper.Map<DataModels.Pet>(request));
            await unitOfWork.SaveAsync();
            return Ok(mapper.Map<Pet>(newPet));
        }
        
        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await unitOfWork.AppRepository.RemovePet(id);
            await unitOfWork.SaveAsync();
            if (pet == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
