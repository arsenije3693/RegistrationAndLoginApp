using Microsoft.AspNetCore.Mvc;
using RegistrationAndLoginApp.Models.ViewModels;
using RegistrationAndLoginApp.Services.BussinesLogicLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

public class LoginController : Controller
{
    private readonly UserLogic _userLogic;

    // Constructor injection
    public LoginController(UserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    // GET: Show login form
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: Handle login
    [HttpPost]
    public async Task<IActionResult> Login(UserViewModel user)
    {
        bool isValidated;
        UserViewModel? validatedUser;

        (isValidated, validatedUser) = _userLogic.ValidateUserCredentials(user.Username, user.Password);

        if (isValidated && validatedUser != null)
        {
            // Create claims
            var claims = new List<Claim>
            {
                new Claim("UserId", validatedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, validatedUser.Username)
            };

            // Add roles/groups if any
            if (validatedUser.GroupPossibilities != null)
            {
                foreach (var group in validatedUser.GroupPossibilities)
                {
                    if (group.IsSelected)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, group.Name));
                    }
                }
            }

            // Create identity & principal
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Sign in user and issue cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal);

            // Redirect to success page (forces navbar reload)
            return RedirectToAction("LoginSuccess");
        }
        else
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View(user);
        }
    }

    // GET: Login success page
    public IActionResult LoginSuccess()
    {
        return View();
    }
}
