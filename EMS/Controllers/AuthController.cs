using EMS.DTO;
using EMS.Model;
using EMS.Repository;
using EMS.Sevice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IRepository<UserData>repository,IConfiguration configuration)
        {
            repo = repository;
            this.configuration = configuration;
        }

        private readonly IRepository<UserData> repo;
        private readonly IConfiguration configuration;

        [HttpPost("Login")]
        public async Task<IActionResult> login(AuthDTO model) {

            var user=(await repo.GetAll(x => x.Email == model.Email )).FirstOrDefault();

            var passwordHelper = new PasswordHelper();

            if (passwordHelper. VerifyPassword(user.Password,model.Password)) { return BadRequest("User or password Incorrect"); }

            var token = GenerateToken(user.Email,user.Password);

            return Ok(new AuthTokenDTO() {
            Id =user.Id,
            Email=user.Email,
            Token= token,
            Role=user.Role,

            }
            );
        }
        private string GenerateToken(string email,string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var credntials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(ClaimTypes.Name,email),
            new Claim(ClaimTypes.Role,role)
            };
            var token = new JwtSecurityToken(
                    claims: claims,
                   expires: DateTime.UtcNow.AddHours(1),
                     signingCredentials: credntials
  );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }


}
