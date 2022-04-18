using Microsoft.AspNetCore.Identity;
using WebApplication2.Models;
namespace WebApplication2
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager) {
            string adminEmail = "admin@gmail.com";
            string password = "Daniill2002!";
            if (await roleManager.FindByNameAsync("admin") == null) {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null) {
                Users admin = new Users { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
