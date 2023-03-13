using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NETCORE.Domain.Users.Domain;
using NETCORE.Domain.Users.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NETCORE.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] RequestLogin loginData)
        {
            if (loginData.Username == "admin" && loginData.Password == "admin")
            {
                var issuer = this._configuration.GetValue<string>("Jwt:Issuer");
                var audience = this._configuration.GetValue<string>("Jwt:Audience");
                var key = Encoding.ASCII.GetBytes(this._configuration.GetValue<string>("Jwt:Key"));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, loginData.Username),
                new Claim(JwtRegisteredClaimNames.Email, loginData.Username),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetUser([FromRoute] string id)
        {
            User user = new User()
            {
                Id = 1,
                Name = "Mi nombre",
                Lastname = "Mi apellido",
                Age = 10
            };

            return Ok(user);
        }
    }
}
