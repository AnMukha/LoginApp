using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();

builder.Services.AddAuthentication().AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SigningKey")!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
    options.MetadataAddress = string.Empty;
});
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // TODO Swagger doesn't work for mapped endpoints. How to Swagger with Ocelot?
    app.UseSwaggerForOcelotUI(options =>
    {
        options.PathToSwaggerGenerator = "/swagger/docs";
    });
}

await app.UseOcelot();

// TODO We should use https in production
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
