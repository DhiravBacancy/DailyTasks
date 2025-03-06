using JwtAuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace JwtAuthApi.Controller
{

    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            Console.WriteLine(123);
            var registeredUser = _authService.Register(user);
            return Ok($"User {registeredUser.Username} is registered");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine(1);

            var authenticatedUser = _authService.Login(loginRequest.Username, loginRequest.Password);
            if (authenticatedUser == null) return Unauthorized();

            var token = _authService.GenerateToken(authenticatedUser);
            return Ok(new { Message = $"User {authenticatedUser.Username} has logged in", Token = token });
        }


        [Authorize] // Ensure only authenticated users can update their details
        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] User User)
        {
            var user = _authService.Update(User);
            if (user == null) return NotFound("User not found");

            return Ok(new { message = "User updated successfully", user });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getAllUsers")]
        public IActionResult ProtectedRoute()
        {
            if(_authService.GetAllUsers() != null) return Ok(_authService.GetAllUsers());
            return NotFound("No users found");
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


}
