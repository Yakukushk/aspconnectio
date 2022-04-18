using Microsoft.AspNetCore.Identity;
namespace WebApplication2.Models
{
    public class Users : IdentityUser
    {
        public int Year { get; set; }
    }
}
