using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIOrderTracking.Guards.Hashers;
using WebAPIOrderTracking.Guards.Interfaces;
using WebAPIOrderTracking.Models.Authefication;
using WebAPIOrderTracking.Models.Entities;

namespace WebAPIOrderTracking.Guards.Logic
{
    public class JwtAuthLogic : IAuthLogic
    {
        private readonly IHasherable _hasher;
        private readonly IConfiguration _config;

        public JwtAuthLogic(IHasherable hasher, IConfiguration configuration)
        {
            _hasher = hasher;
            _config = configuration;
        }

        public bool TryLogin(User user, LoginModel loginModel, out string result)
        {
            if (_hasher.Verify(loginModel.Password, user.Userpassword))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Secret:jwt")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                issuer: "https://www.ordertracking.somee.com",
                audience: "https://www.ordertracking.somee.com",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
                );
                result = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return true;
            }
            else
            {
                result = "Неправильный пароль";
                return false;
            }
        }
    }
}
