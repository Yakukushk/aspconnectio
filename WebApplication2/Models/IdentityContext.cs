using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Models
{
    public class IdentityContext : IdentityDbContext<Users> 
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
    

