using LoginApp.Services.Identity.Entities;
using LoginApp.Services.Identity.Infrastructure;
using LoginApp.Services.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.Services.Identity.Repositories
{
    public class UsersRepository(IdentityDbContext identityDbContext) : IUsersRepository
    {
        private readonly IdentityDbContext _dbContext = identityDbContext;

        public async Task<User?> FindUserByName(string userName)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
