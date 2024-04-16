using LoginApp.Services.Identity.Dtos;

namespace LoginApp.Services.Identity.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(string username, string password);
    }
}
