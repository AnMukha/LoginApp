using LoginApp.Services.Identity.Entities;
using LoginApp.Services.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
optionsBuilder.UseNpgsql("Host=localhost:5432;Database=LoginApp;Username=postgres;Password=Admin1#");

using (var context = new IdentityDbContext(optionsBuilder.Options))
{
    var existedUser = context.Users.FirstOrDefault(u => u.UserName == "user1");
    if (existedUser != null)
    {
        context.Users.Remove(existedUser);
    }
    var user = new User() { UserName = "user1", Email = "email1", Id = Guid.NewGuid()};
    var hasher = new PasswordHasher<User>();
    user.PasswordHash = hasher.HashPassword(user, "user1");

    context.Users.Add(user);
    context.SaveChanges();
}
