using Company_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> usermanager,
            SignInManager<IdentityUser> signinmanager)
        {
            _userManager = usermanager;
            _signInManager = signinmanager;
        }

        // ... Register and Login actions follow
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = new IdentityUser
            {
                Email = vm.Email,
                UserName = vm.Email
            };

            // CreateAsync hashes the password automatically — we NEVER store plain
            // text password
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                // Automatically sign the new user in right after registering
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(vm);
        }

        [HttpGet] 
        public IActionResult Login(string? returnUrl = null) { 
            ViewBag.ReturnUrl = returnUrl; 
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                // Avoid open-redirect vulnerabilities — only redirect to LOCAL urls
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Account locked due to too many failed attempts. Try again later.");

                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
