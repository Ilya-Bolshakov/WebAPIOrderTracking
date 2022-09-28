using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIOrderTracking.Guards;
using WebAPIOrderTracking.Models.Authefication;
using WebAPIOrderTracking.Models.Entities;

namespace WebAPIOrderTracking.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly OrderTrackingContext _context;

        public AuthController(OrderTrackingContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            var users = _context.Users;

            var findUser = users.FirstOrDefault(u => u.Username == user.UserName);


            if (findUser != null)
            {   
                if (SecretHasher.Verify(user.Password, findUser.Userpassword))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                    issuer: "https://www.ordertracking.somee.com",
                    audience: "https://www.ordertracking.somee.com",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new AuthenticatedResponse { Token = tokenString });
                }
            }
            return Unauthorized();
        }
    }
}
