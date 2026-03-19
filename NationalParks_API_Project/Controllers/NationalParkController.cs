using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParks_API_Project.Models;
using NationalParks_API_Project.Models.DTOs;
using NationalParks_API_Project.Repository;
using NationalParks_API_Project.Repository.IRepository;

namespace NationalParks_API_Project.Controllers
{
    [Route("api/NationalPark")]
    [ApiController]
    //[Authorize]
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalParkRepository,
            IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var nationalParkDto = _nationalParkRepository.GetNationalParks().Select(_mapper.Map<NationalPark, NationalParkDto>);
            return Ok(nationalParkDto);//200
        }
        [HttpGet("{nationalParkId:int}",Name ="GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalParkId);
            if(nationalPark == null) return NotFound(); //404
            var nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalParkDto);  //200
        }
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest(); //400
            if(_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "NationalPark Park In Db !!!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest(); //400
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.CreateNationalPark(nationalPark)) 
            {
                ModelState.AddModelError("",$"Something went wrong while create NP : {nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //return Ok();  //200
            return CreatedAtRoute("GetNationalPark",
                new { nationalParkId = nationalPark.Id }, nationalPark); //201
        }
        [HttpPut]
        public IActionResult UpdateNationalPark([FromBody]NationalParkDto natinalParkDto)
        {
            if(natinalParkDto == null) return BadRequest(); 
            if(!ModelState.IsValid) return BadRequest();
            var nationalPark = _mapper.Map<NationalParkDto, NationalPark>(natinalParkDto);
            if(!_nationalParkRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "Something went wrong while update NP:{ nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent(); // 204
        }
        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepository.NationalParkExists(nationalParkId))
                return NotFound();
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalParkId);
            if(nationalPark ==  null) return NotFound();
            if (!_nationalParkRepository.DeleteNationalPark(nationalPark)) 
            {
                ModelState.AddModelError("", "Something went wrong while delete NP:{ nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
