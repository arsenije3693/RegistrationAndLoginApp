using Microsoft.AspNetCore.Mvc;
using RegistrationAndLoginApp.Models.ViewModels;
using RegistrationAndLoginApp.Services.BussinesLogicLayer;

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
   
    public IActionResult Login(UserViewModel user)
    {
        bool isValidated;
        UserViewModel? validateUser;

        (isValidated, validateUser) = _userLogic.ValidateUserCredentials(user.Username, user.Password);

        if (isValidated)
            return View("LoginSuccess");
        else
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View(user); 
        }
    }

}

