using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminUserName = "admin";
            string adminEmail = "admin@gmail.com";
            string password = "Qwerty_1";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("editor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("editor"));
            }
            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                Users admin = new Users { Email = adminEmail, UserName = adminUserName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }

    }
}