using fpt_backend.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace fpt_backend.Tests.UnitTests.Database_Tests;

public class ConnectionTests
{
    public class DbConnectionTests
    {
        [Fact]
        public void CanConnectToDatabase()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            var options = new DbContextOptionsBuilder<FtpDbContext>()
                .UseSqlServer(config.GetConnectionString("DevConString"))
                // Enable detailed EF logging
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;

            using var context = new FtpDbContext(options);

            try
            {
                bool canConnect = context.Database.CanConnect();
                Assert.True(canConnect, "Could not connect to the database.");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Database connection failed.\n" +
                    $"Connection string: {config.GetConnectionString("DevConString")}\n" +
                    $"Error: {ex.Message}\n" +
                    $"Inner: {ex.InnerException?.Message}",
                    ex
                );
            }
        }
    }
}