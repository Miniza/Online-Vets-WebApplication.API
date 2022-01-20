using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineVetAPI.DomainModels;
using OnlineVetAPI.Interfaces;

namespace OnlineVetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        //Dependency injection and field assignment
        public OwnersController(IUnitOfWork unitOfWork, IMapper mapper, IImageRepository imageRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwner()
        {
            var owners =  await unitOfWork.AppRepository.GetOwnersAsync(); //From DataModels Owner
            await unitOfWork.SaveAsync();
            return Ok(mapper.Map<IEnumerable<Owner>>(owners)); // To DomainModels Owner
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int Id)
        {
            var owner = await unitOfWork.AppRepository.GetOwnerAsync(Id); //From DataModels Owner through repository
            if (owner == null)
            {
                return NotFound();
            }
            await unitOfWork.SaveAsync();
            return Ok(mapper.Map<Owner>(owner));   //To DomainModels Owner
        }
       
        
        // PUT: api/Owners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] 
        public async Task<IActionResult> PutOwner(int id, [FromForm] UpdateOwner request)
        {
            if (await unitOfWork.AppRepository.Exists(id))  //Check if owner exists
            {
               //create a var updated owner 
               var updatedOwner = await unitOfWork.AppRepository.UpdateOwner(id, mapper.Map<DataModels.Owner>(request));
                await unitOfWork.SaveAsync();
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
            var newOwner = await unitOfWork.AppRepository.AddOwner(mapper.Map<DataModels.Owner>(request));
            await unitOfWork.SaveAsync();
            return Ok(mapper.Map<Owner>(newOwner));
        } 
       

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await unitOfWork.AppRepository.RemoveOwner(id);
            await unitOfWork.SaveAsync();
            if (owner == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UploadProfile(int id, IFormFile profilepic)
        {
            //check if owner exists
            if (await unitOfWork.AppRepository.Exists(id))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profilepic.FileName);
                //Upload image to local storage
               var filePath = await imageRepository.Upload(profilepic,fileName);

                //update the path (url) in the database 
                if (await unitOfWork.AppRepository.UpdateProfileImage(id, filePath))
                { 
                    return Ok(filePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Uploading image");
            }
            return NotFound();
        }


    }
}
