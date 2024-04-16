using LoginApp.RestServicesCore;
using LoginApp.Services.Identity.Infrastructure;
using LoginApp.Services.Identity.Interfaces;
using LoginApp.Services.Identity.Repositories;
using LoginApp.Services.Identity.Services;
using Microsoft.EntityFrameworkCore;

var service = new RestServicesCore(args);

service.Start<Program>(builder =>
{
    builder.Services.AddDbContext<IdentityDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddTransient<ILoginService, LoginService>();
    builder.Services.AddTransient<IUsersRepository, UsersRepository>();
});

