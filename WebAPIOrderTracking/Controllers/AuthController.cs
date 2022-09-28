using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(new { message = "Invalid client request" });
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
                    return Conflict(new { message = result });
                }
                
            }
            else
            {
                return Conflict(new { message = "Неправильный логин" });
            }
            
        }
    }
}
