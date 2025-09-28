/*
 * Arsenije Brajovic
 * CST - 350
 * 9/19/2025
 * Registration and Login - Group View Model
 * Activity 2 - Part 1
 */


namespace RegistrationAndLoginApp.Models.ViewModels
{
    public class GroupViewModel
    {
        internal bool IsSelected;

        // class properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean isSelected { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public GroupViewModel()
        {   // Initialize class properties
            Id = 0;
            Name = string.Empty;
            isSelected = false;
        }
    }
}
