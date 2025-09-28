/*
 * Arsenije Brajovic
 * CST - 350
 * 9/19/2025
 * Registration and Login - View Model
 * Activity 2 - Part 1
 */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistrationAndLoginApp.Models.ViewModels
{
    // ViewModel for user registration and login
    public class UserViewModel
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // Username of the user
        // Required - cannot be empty
        // StringLength - must be between 3 and 20 characters
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be 3-20 characters")]
        public string Username { get; set; }

        // Password of the user
        // Required - cannot be empty
        // StringLength - at least 6 characters, max 100 for security
        // DataType.Password - masks input in UI
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Phone number of the user
        // Required - cannot be empty
        // Phone - validates correct phone format
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        // User's date of birth
        // Required - cannot be empty
        // DataType.Date - renders as a date picker in UI
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        // Selected user group
        // Required - must choose one group
        [Required(ErrorMessage = "Please select a group")]
        public int SelectedGroupId { get; set; }

        // List of all available groups to choose from
        // This is used to render radio buttons on the registration form
        public List<GroupViewModel> GroupPossibilities { get; set; } = new List<GroupViewModel>();
    }
}

