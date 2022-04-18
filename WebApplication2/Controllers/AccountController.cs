using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.ModelView;


namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signManager;
        public AccountController(UserManager<Users> userManager, SignInManager<Users> signManager) { //constructor for async controllers with accounts manager 
            _userManager = userManager;
            _signManager = signManager;
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel model) { // async each others users 
            if (ModelState.IsValid) {
            Users user = new Users {Email = model.Email, UserName = model.Email, Year=model.Year};// add users 
                var result = await _userManager.CreateAsync(user, model.Password); // give results 
                if (result.Succeeded) { //conditionals in order to success 
                await _signManager.SignInAsync(user,false); //install cookie for web 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors) { // if we have any issues and fix it 
                    ModelState.AddModelError(string.Empty, error.Description);
                    }

                }

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null) {
        return View(new LoginViewModel {ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        { // action task for async data 
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false); // result sign in 
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect!");
                }

            }
            return View(model);
        }
            [HttpPost]
            [ValidateAntiForgeryToken]
            
            public async Task<IActionResult> Logout()
            {
            await _signManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
      
    }
    }

