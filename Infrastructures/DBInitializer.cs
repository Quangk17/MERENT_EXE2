using Domain.Entites;

namespace Infrastructures
{
    public static class DBInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {

            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "User" },
                    new Role { Name = "Admin" }
                };

                foreach (var role in roles)
                {
                    await context.Roles.AddAsync(role);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
