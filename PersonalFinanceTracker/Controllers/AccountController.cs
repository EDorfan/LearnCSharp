/* Controller handles registration, login and logout functions

The AccountController is responsible for handling user authentication. It allows users to:

✅ Register → Create a new account
✅ Login → Sign in with their credentials
✅ Logout → Sign out of their session

This controller interacts with ASP.NET Identity, which is a built-in authentication system in ASP.NET Core. It uses UserManager, SignInManager, and RoleManager to manage users and their roles.

*/

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    // UserManager, SignInManager, and RoleManager are services provided by ASP.NET Identity
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    // Constructor - Dependency injection is used to inject UserManager, SignInManager, and RoleManager services
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    // GET: /Account/Register
    // Returns the Register view
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    // Handles the registration process
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    // GET: /Account/Login
    // Returns the Login view
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    // Handles the login process
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }
        return View(model);
    }

    // POST: /Account/Logout
    // Handles the logout process
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}