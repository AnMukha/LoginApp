using LoginApp.Services.Identity.Dtos;
using LoginApp.Services.Identity.Entities;
using LoginApp.Services.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginApp.Services.Identity.Services
{
    public class LoginService(IConfiguration configuration, IUsersRepository usersRepository) : ILoginService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUsersRepository _usersRepository = usersRepository;

        private readonly TimeSpan defaultTokenExpirationTime = TimeSpan.FromDays(7);

        public async Task<LoginResponse> Login(string userName, string password)
        {
            var user = await _usersRepository.FindUserByName(userName);
            if (user == null)
            {
                return new LoginResponse() { Success = false, UserName = userName };
            }

            if (!PasswordVerified(user, password))
            {
                return new LoginResponse() { Success = false, UserName = userName };
            }
            
            var token = GetToken(user);

            return new LoginResponse()
            {
                Success = true,
                UserName = user.UserName,
                Token = token,
                Email = user.Email,
            };
        }

        private string GetToken(User user)
        {
            var signingKeyValue = _configuration.GetValue<string>("SigningKey");
            if (string.IsNullOrWhiteSpace(signingKeyValue))
            {
                throw new Exception("Signing key not found in application configuration.");
            }
            var key = Encoding.ASCII.GetBytes(signingKeyValue);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow + defaultTokenExpirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static bool PasswordVerified(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            var verificationResult = hasher.VerifyHashedPassword(user, user.PasswordHash!, password);
            return verificationResult != PasswordVerificationResult.Failed;
        }
    }
}
