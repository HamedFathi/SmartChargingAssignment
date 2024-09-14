
using HamedStack.AspNetCore.Endpoint;
using HamedStack.TheResult.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SCA.Application;
using SCA.Infrastructure;

namespace SCA.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddMinimalApiEndpoints();

        builder.Services.AddProblemDetails();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddInfrastructure<SmartChargingAssignmentContext>();
        builder.Services.AddDbContext<SmartChargingAssignmentContext>(options =>
            options.UseSqlite("Data Source=smartchargingdb.db"));

        builder.Services.AddApplication();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ResultExceptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();
        app.MapMinimalApiEndpoints();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<SmartChargingAssignmentContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<SCA.Presentation.Program>>();
                logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
            }
        }

        app.Run();
    }
}