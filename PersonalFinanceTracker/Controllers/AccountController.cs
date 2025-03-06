/* Controller handles registration, login and logout functions

The AccountController is responsible for handling user authentication. It allows users to:

✅ Register → Create a new account
✅ Login → Sign in with their credentials
✅ Logout → Sign out of their session

This controller interacts with ASP.NET Identity, which is a built-in authentication system in ASP.NET Core. It uses UserManager, SignInManager, and RoleManager to manage users and their roles.

*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PersonalFinanceTracker.Models;

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
        Console.WriteLine("Received a POST request for Register");

        if (ModelState.IsValid)
        {
            Console.WriteLine("Model is valid");

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            Console.WriteLine($"Attempting to create user: {user.UserName}");

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                Console.WriteLine("User Successfully Registered");
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}"); // Debugging output
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

    /*
    Requires the user to be authorized ([Authorize]) to access this action.
    Retrieves the current user's email using _userManager.GetUserAsync(User).Result.
    Creates a new instance of ProfileViewModel with the user's email.
    Returns a view with the populated ProfileViewModel instance.
    */
    [Authorize]
    public IActionResult Profile()
    {
        var user = _userManager.GetUserAsync(User).Result;
        var model = new ProfileViewModel { Email = user.Email };
        return View(model);
    }

    /*
    update a user's profile. Here's a succinct breakdown:

    It checks if the model state is valid. If not, it returns the view with the model.
    It retrieves the current user using _userManager.GetUserAsync(User).
    If the CurrentPassword and NewPassword fields in the model are not null, it attempts to change the user's password using _userManager.ChangePasswordAsync.
    If the password change fails, it adds error messages to the model state and returns the view with the model.
    If the password change succeeds or if no password change was attempted, it redirects the user to the "Index" action of the "Home" controller.

    */
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        
        if (model.CurrentPassword != null && model.NewPassword != null)
        {
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        return RedirectToAction("Index", "Home");
    }



}