using Microsoft.AspNetCore.Mvc;
using RegistrationAndLoginApp.Models.ViewModels;
using RegistrationAndLoginApp.Services.BussinesLogicLayer;

namespace RegistrationAndLoginApp.Controllers
{
    /// <summary>
    /// Controller to handle register actions
    /// </summary>
    public class RegisterController : Controller
    {
        private readonly UserLogic _userLogic;

        public RegisterController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        // GET: Show registration form
        [HttpGet]
        public IActionResult Register()
        {
            var viewUser = new UserViewModel
            {
                GroupPossibilities = _userLogic.GetAllGroups()
            };

            return View(viewUser);
        }

        // POST: Handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                // Reload groups in case of validation error
                user.GroupPossibilities = _userLogic.GetAllGroups();
                return View(user);
            }

            // Add the user
            _userLogic.AddUser(user);

            // Redirect to Home or Login page
            return RedirectToAction("Index", "Home");
        }
    }
}
