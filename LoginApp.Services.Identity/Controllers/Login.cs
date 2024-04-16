using FluentValidation;
using LoginApp.Services.Identity.Dtos;
using LoginApp.Services.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Services.Identity.Controllers
{

    [ApiController]
    [Route("api/login")]
    public class LoginController(ILoginService loginService, IValidator<Credentials> credValidator) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;
        private readonly IValidator<Credentials> _credValidator = credValidator;

        [HttpPost]
        public async Task<IResult> Login(Credentials credentials)
        {
            var validationResult = await _credValidator.ValidateAsync(credentials);
            if (!validationResult.IsValid)
            {                
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            var result = await _loginService.Login(credentials.UserName!, credentials.Password!);
            return Results.Ok(result);
        }

    }
}
