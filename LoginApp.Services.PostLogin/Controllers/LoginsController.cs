using LoginApp.Services.PostLogin.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Services.PostLogin.Controllers
{

    [ApiController]
    [Route("api/post-login/logins")]
    public class LoginsController : ControllerBase
    {
        public LoginsController()
        {
        }

        const string consoleAppDescriptor = "console";

        [HttpPost]        
        public Task<IResult> Post([FromBody] ClientInfo clientInfo)
        {
            if (clientInfo.ClientType == consoleAppDescriptor)
            {
                Console.WriteLine("------------------------- Logined from console client ------------------------------");
                // TODO ... some async code to store login info
            }
            return Task.FromResult(Results.Ok());
        }

    }

}
