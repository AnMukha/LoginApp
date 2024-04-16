using FluentValidation;

namespace LoginApp.RestServicesCore
{
    // !!!!!!!!!!!!!!   This is just something to start creating framework, absoulitely not ready to use and wrong
    public class RestServicesCore
    {

        private readonly string[]? _args = null;
        public RestServicesCore(string[]? args = null)
        {
            _args = args;
        }

        public void Start<AnyClassFormAssembly>(Action<WebApplicationBuilder>? registerServices = null)
        {
            var builder = _args==null ? WebApplication.CreateBuilder() : WebApplication.CreateBuilder(_args);

            builder.Services.AddControllers();
            object value = builder.Services.AddValidatorsFromAssemblyContaining<AnyClassFormAssembly>();

            builder.Services.AddTransient<ExceptionHandlerMiddleware>();

            registerServices?.Invoke(builder);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapControllers();

            app.Run();

        }
    }
}
