using Domain.Entites;
using Microsoft.EntityFrameworkCore;


namespace Infrastructures
{
    public class AppDBContext : IdentityDbContext<User, Role>
    {
        public StudentEventForumDbContext(DbContextOptions options) : base(options)
        {


        }

        
       
    }

}
