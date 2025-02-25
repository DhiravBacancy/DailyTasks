using JwtAuthApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;  

namespace JwtAuthApi
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager _userManager;

        public AuthService(IConfiguration config, UserManager userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User Register(User user)
        {
            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            var NewUser = new User { 
                Id = _userManager.GetUsers().Count + 1, 
                Username = user.Username, 
                Password = user.Password, 
                Age = user.Age,
                Email = user.Email,
                Role = user.Role
            };
            _userManager.AddUser(NewUser);
            return NewUser;
        }

        public User Login(string username, string password)
        {
            Console.WriteLine(2);
            var user = _userManager.GetUserByUsername(username);
            if (user != null && (password == user.Password))
            {
                return user;
            }
            return null;
        }

        
        public User Update(User user)
        {
            User UpdatedUser =  _userManager.UpdateUser(user);
            return UpdatedUser;
        }

        public List<User> GetAllUsers()
        {
            return _userManager.GetUsers();
        }

    }
}
