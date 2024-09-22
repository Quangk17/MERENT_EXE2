using Infrastructures;
using Microsoft.EntityFrameworkCore;

namespace MERENT_API.Middlewares
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app, ILogger _logger)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();

                using AppDbContext dbContext =
                    scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.Migrate();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An problem occurred during migration!");
            }
        }
    }
}
