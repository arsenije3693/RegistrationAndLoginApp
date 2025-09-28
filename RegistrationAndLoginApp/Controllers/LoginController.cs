/*
 * Arsenije Brajovic
 * CST - 350    
 * Register and Login App
 * Activity 2 guide part 2
 * 
 * 
 */

using Microsoft.AspNetCore.Mvc;
using RegistrationAndLoginApp.Models.ViewModels;
using RegistrationAndLoginApp.Services.BussinesLogicLayer;

namespace RegistrationAndLoginApp.Controllers
{
    public class LoginController : Controller
    {

        private UserLogic _userLogic;

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET method to show the login form
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST method to process the login attempt
        /// </summary>
        [HttpPost]
        public IActionResult Login(UserViewModel user)
        {
            // declare and initialize
            bool isValidated = false;
            UserViewModel? validateUser;

            //call the userlogic method
            (isValidated, validateUser) = _userLogic.ValidateUserCredentials(user.Username, user.Password);
            // if isValidated is true, redirect to success page
            //else to the the login failure apge
            if(isValidated)
            {
                return View("LoginSuccess");
            }
            else
            {
                return View("LoginFailure");
            }
        }
    }
}
