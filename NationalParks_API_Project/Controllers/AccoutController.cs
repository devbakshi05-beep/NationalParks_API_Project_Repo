using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParks_API_Project.Models;
using NationalParks_API_Project.Repository.IRepository;

namespace NationalParks_API_Project.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccoutController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccoutController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            if(ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(user.UserName);
                if(!isUniqueUser) return BadRequest("User in use !!");
                var userInfo = _userRepository.Register(user.UserName, user.Password);
                if (userInfo == null) return NotFound();
                user = userInfo;

            }
            return Ok(user);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var userInDb = _userRepository.Authenticate(user.UserName, user.Password);
            if (userInDb == null) return BadRequest("Wrong user/password !!!");
            return Ok(userInDb);
        }
    }
}
