
using MiddlewareAuth;
using MomoApi.CustomMiddleware;
using MomoApi.Utils;

namespace MomoApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = new Configuration();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton(config);
        builder.Services.AddSingleton<IMerchantValidation>(new MerchantValidation(config.get("DbString").ToString()));
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<MerchantValidationMiddleware>();

        app.MapControllers();

        app.Run();
    }
}

