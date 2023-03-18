using BasicsOfWebApi.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BasicsOfWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserLogin userLogin)
        {
            var user = GetUser(userLogin);

            if(user != null)
            {
                var token = GenetareToken(user);

                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string GenetareToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            var securtyHandler = new JwtSecurityTokenHandler();
            var encryptedToken = securtyHandler.WriteToken(token);
            return encryptedToken;
        }

        private UserModel GetUser(UserLogin userLogin)
        {
            return UsersDataStore.Users.FirstOrDefault(user => user.UserName.ToLower() == userLogin.UserName.ToLower()
                        && user.Password == userLogin.Password);
        }
    }
}
