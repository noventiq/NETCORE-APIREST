using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NETCORE.Application.Users;
using NETCORE.Domain.Users.Domain;
using NETCORE.Domain.Users.DTO;
using NETCORE.Shared;
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
        private UserApp _userApp;
        public UsersController(UserApp userApp, IConfiguration configuration)
        {
            this._userApp = userApp;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] RequestLogin loginData)
        {
            StatusResponse<User> status = await this._userApp.Login(loginData.Username, loginData.Password);

            if(!status.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, status);
            }

            var issuer = this._configuration.GetValue<string>("Jwt:Issuer");
            var audience = this._configuration.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(this._configuration.GetValue<string>("Jwt:Key"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, status.Data.Name),
                new Claim(JwtRegisteredClaimNames.Email, status.Data.Lastname),
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
