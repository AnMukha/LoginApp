using LoginApp.Services.Identity.Entities;

namespace LoginApp.Services.Identity.Interfaces
{
    public interface IUsersRepository
    {
        Task<User?> FindUserByName(string userName);
    }
}
