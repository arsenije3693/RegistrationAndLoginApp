/*
 * Arsenije Brajovic
 * CST-350
 * 9/17/2025
 * Register and Logic App Domain Model
 * Activity 2 - Part 1
 */

using System;
using System.Collections.Generic;

namespace RegistrationAndLoginApp.Models.DomainModels
{
    public class UserDomainModel
    {
        // class level properties

        // Id - unique identifier for the user
        public int Id { get; set; }

        // Username - the user's login name
        public string Username { get; set; }

        // Password - the user's password
        public string Password { get; set; }

        // Groups - list of groups that the user belongs to
        public List<GroupDomainModel> Groups { get; set; }

        // PhoneNumber - new field to store the user's phone number
        public string PhoneNumber { get; set; }

        // DateOfBirth - new field to store the user's date of birth
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public UserDomainModel()
        {
            // Declare default values for the properties
            Id = 0;
            Username = string.Empty;
            Password = string.Empty;
            Groups = new List<GroupDomainModel>();
            PhoneNumber = string.Empty;
            DateOfBirth = null;
        }
    }
}
