using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIOrderTracking.Guards.Hashers;
using WebAPIOrderTracking.Guards.Interfaces;
using WebAPIOrderTracking.Models.Authefication;
using WebAPIOrderTracking.Models.Entities;

namespace WebAPIOrderTracking.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly OrderTrackingContext _context;
        private readonly IAuthLogic _authLogic;

        public AuthController(OrderTrackingContext context, IAuthLogic logic)
        {
            _context = context;
            _authLogic = logic;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
            var users = _context.Users;

            var user = users.FirstOrDefault(u => u.Username == loginModel.UserName);


            if (user is User u)
            {
                if (_authLogic.TryLogin(user, loginModel, out string result))
                {
                    return Ok(new AuthenticatedResponse { Token = result });
                }
                else
                {
                    return Conflict(new { error = result });
                }
                
            }
            else
            {
                return Conflict(new { error = "Неправильный логин" });
            }
            
        }
    }
}
