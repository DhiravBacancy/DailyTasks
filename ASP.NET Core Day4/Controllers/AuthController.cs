using ASP.NET_Core_Day4.Models;
using ASP.NET_Core_Day4.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Day4.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FileService _fileService;
        private readonly CacheService _cacheService;

        public AuthController(FileService fileService, CacheService cacheService)
        {
            _fileService = fileService;
            _cacheService = cacheService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            Console.WriteLine("Register endpoint hit");
            if (string.IsNullOrEmpty(user?.Username))
                return BadRequest("Username is required.");

            user.Guid = Guid.NewGuid().ToString();
            _fileService.SaveUser(user);
            _cacheService.StoreGuid(user.Guid);

            return Ok(new { Message = "User registered successfully", GUID = user.Guid });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            if (string.IsNullOrEmpty(user?.Username))
                return BadRequest("Username is required.");

            var users = _fileService.GetAllUsers();
            var existingUser = users.Find(u => u.Username == user.Username);

            if (existingUser == null)
                return Unauthorized("Invalid username.");

            _cacheService.StoreGuid(existingUser.Guid);

            return Ok(new { Message = "Login successful", GUID = existingUser.Guid });
        }
    }
}
