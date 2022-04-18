using Microsoft.AspNetCore.Mvc;
using WebApplication2.ModelView;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RolesController : Controller
    {
         RoleManager<IdentityRole> _roleManager;
         UserManager<Users> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<Users> userManager) { // constructor values 
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => View(_roleManager.Roles.ToList()); // action method for list of roles
        public IActionResult Userlist() => View(_userManager.Users.ToList()); // action method for list of users 
        public async Task<IActionResult> Edit(string userid) {  // try to async users into data base 
            Users users = await _userManager.FindByIdAsync(userid);
            if (users != null) {
                var userRoles = await _userManager.GetRolesAsync(users);
                var allRoles = _roleManager.Roles.ToList();
                RoleViewModel model = new RoleViewModel // sharing with values from class RoleViewModel
                {
                    UsersID = users.Id,
                    UsersEmail = users.Email,
                    UsersRoles = userRoles,
                    Roles = allRoles
                };
                return View(model);

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userid, List<string> roles) { // try to async roles and users into data base 
            Users users = await _userManager.FindByIdAsync(userid);
            if (users != null) {
                var usersRoles = await _userManager.GetRolesAsync(users);
                var AllRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(usersRoles); // method for adding 
                var removedRoles = usersRoles.Except(roles); // method for removing 
                await _userManager.AddToRolesAsync(users, addedRoles);
                await _userManager.RemoveFromRolesAsync(users, removedRoles);
                return RedirectToAction("UserList");
            }
            return NotFound();
        }
    
    }
}
      

