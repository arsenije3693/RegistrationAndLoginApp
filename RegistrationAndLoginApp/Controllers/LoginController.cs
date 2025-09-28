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

namespace RegistrationAndLoginApp.Controllers
{
    public class LoginController : Controller
    {
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
            if (!ModelState.IsValid)
                return View(user);

            // TODO: Add authentication logic here

            return RedirectToAction("Index", "Home");
        }
    }
}
