using BasicsOfWebApi.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BasicsOfWebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UsersController(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetCuurentUser()
        {
            var headers = _contextAccessor.HttpContext.Request.Headers;

            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return NoContent();
            }

            var userClaims = identity.Claims;

            var userFromClaims = new UserModel
            {
                UserName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                FirstName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value.Split(" ")[0],
                LastName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value.Split(" ")[1],
                Role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
            };

            return Ok(userFromClaims);
        }


        [HttpGet("GetAll")]
        [AllowAnonymous]
        public IActionResult GetAllUsers()
        {
            return Ok(UsersDataStore.Users.ToList());
        }

        [HttpGet("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetForAdminOnly()
        {
            return Ok("You are An Admin");
        }
    }
}
