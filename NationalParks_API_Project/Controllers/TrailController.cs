using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParks_API_Project.Models;
using NationalParks_API_Project.Models.DTOs;
using NationalParks_API_Project.Repository.IRepository;

namespace NationalParks_API_Project.Controllers
{
    [Route("api/trail")]
    [ApiController]
    //[Authorize]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;
        public TrailController(ITrailRepository trailRepository,
            IMapper mapper)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetTrails()
        {
            return Ok(_trailRepository.GetTrails().Select(_mapper.Map<Trail,TrailDto>));
        }
        [HttpGet ("{trailId:int}",Name ="GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepository.GetTrail(trailId);
            if (trail == null) return NotFound();
            var trailDto = _mapper.Map<TrailDto>(trail);
            return Ok(trailDto);
        }
        [HttpPost]
        public IActionResult CreateTrail([FromBody]TrailDto trailDto)
        {
            if (trailDto == null) return BadRequest();
            if(_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail in DB !!!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if(!ModelState.IsValid) return BadRequest();
            var trail = _mapper.Map<TrailDto,Trail>(trailDto);
            if(!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("",
                    $"Something went wrong while create trail : {trail:Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return CreatedAtRoute("GetTrail",new {trailId = trail.Id},trail);
        }
        [HttpPut]
        public IActionResult UpdateTrail([FromBody] TrailDto trailDto)
        {
            if (trailDto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var trail = _mapper.Map<TrailDto, Trail>(trailDto);
            if (!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("",
                    $"Something went wrong while update trail : {trail:Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepository.TrailExists(trailId)) return NotFound();
            var trail = _trailRepository.GetTrail(trailId);
            if(trail == null) return NotFound();
            if(!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("",
                    $"Something went wrong while update trail : {trail:Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
