using WebApplication2.Models;
using WebApplication2.ModelView;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Users> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<Users> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index() => View(_roleManager.Roles.ToList());
        [Authorize(Roles = "admin")]
        public IActionResult UserList() => View(_userManager.Users.ToList());
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var allRoles = _roleManager.Roles.ToList();

            var roleUser = await _roleManager.FindByNameAsync("user");

            var indexRoles = allRoles.Except(new List<IdentityRole>() { roleUser }).ToList();

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new RoleViewModel()
            {
                UsersEmail = user.UserName,
                UsersID = user.Id,
                UsersRoles = userRoles,
                Roles = indexRoles
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();
            var userRoles = await _userManager.GetRolesAsync(user);

            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);

            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("UserList");
        }
    }

}
