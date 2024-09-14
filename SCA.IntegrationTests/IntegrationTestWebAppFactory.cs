using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SCA.Infrastructure;

namespace SCA.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<SCA.Presentation.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<SmartChargingAssignmentContext>), typeof(DbConnection));

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<SmartChargingAssignmentContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });

            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SmartChargingAssignmentContext>();

            // db.Database.EnsureDeleted(); // For SQLite in-memory is not necessary.
            db.Database.EnsureCreated();

            builder.UseEnvironment("Development");
        });
    }
}