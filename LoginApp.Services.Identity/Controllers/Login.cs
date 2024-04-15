using LoginApp.Services.Identity.Dtos;
using LoginApp.Services.Identity.Entities;
using LoginApp.Services.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginApp.Services.Identity.Controllers
{

    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        IdentityDbContext _dbContext;
        IConfiguration _configuration;

        public LoginController(IdentityDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<LoginResponse> Login(Credentials credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.UserName) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return new LoginResponse() { Success = false, UserName = credentials.UserName };
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == credentials.UserName);
            if (user == null) 
            {
                return new LoginResponse() { Success = false, UserName = credentials.UserName };
            }
            var hasher = new PasswordHasher<User>();
            var verificationResult = hasher.VerifyHashedPassword(user, user.PasswordHash!, credentials.Password!);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return new LoginResponse() { Success = false, UserName = credentials.UserName };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SigningKey")!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new LoginResponse()
                {
                    Success = true,
                    UserName = credentials.UserName,
                    Token = tokenHandler.WriteToken(token),
                };
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

    }
}
