/*
 * Arsenije Brajovic
 * CST - 350
 * 9/19/2025
 * Registration and Login - View Model
 * Activity 2 - Part 1
 */


using System.ComponentModel.DataAnnotations;

namespace RegistrationAndLoginApp.Models.ViewModels
{

    public class UserViewModel
    {
      

        //Class properties

        public int Id { get; set; }


        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public List<GroupViewModel> GroupPossibilities { get; set; }

        [Required(ErrorMessage = "Please select a group.")]
        public int SelectedGroupId { get; set; }    






        /// <summary>
        /// Default constructor
        /// </summary>
        public UserViewModel()
        {
            // Initialize class properties
            Id = 0;
            Username = string.Empty;
            Password = string.Empty;
            GroupPossibilities = new List<GroupViewModel>();
        }



    }
}
