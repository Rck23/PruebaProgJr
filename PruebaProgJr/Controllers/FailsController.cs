using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FailsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FailsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //GET: api/Fails
        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<IEnumerable<FailDto>>> Get()
        {
            var register = await _unitOfWork.fails.GetAllAsync();


            return _mapper.Map<List<FailDto>>(register);

        }

        //GET: api/Fails/id 
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<FailDto>> Get(int id)
        {
            var register = await _unitOfWork.fails.GetByIdAsync(id);

            if (register == null)
            {
                return NotFound(new ApiResponse(404, "El registro solicitado no existe."));
            }
            return _mapper.Map<FailDto>(register);
        }

        //POST: api/Fails
        [HttpPost(Name ="CreateFail")]
        public async Task<ActionResult<Fail>> Post(FailDto dto )
        {
            // MAPEAR EL PRODUCTO
            var register = _mapper.Map<Fail>(dto);

            _unitOfWork.fails.Add(register);
            await _unitOfWork.SaveAsync();

            if (register == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            dto.Id = register.Id;
            return CreatedAtAction(nameof(Post), new { id = dto.Id }, dto);
        }

        

        //DELETE: api/Fails/id 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var register = await _unitOfWork.fails.GetByIdAsync(id);

            if (register == null)
            {
                return NotFound(new ApiResponse(404, "El registro solicitado no existe."));
            }

            _unitOfWork.fails.Remove(register);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}
