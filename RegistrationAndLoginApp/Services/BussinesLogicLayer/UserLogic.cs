/*
 * Arsenije Brajovic
 * CST-350
 * 9/17/2025
 * Register and Logic - BLL
 * Activity 2 - Part 1
 */
using RegistrationAndLoginApp.Models.DomainModels;
using RegistrationAndLoginApp.Models.ViewModels;
using RegistrationAndLoginApp.Services.Interfaces;
using RegistrationAndLoginApp.Services.Mappers;
using System;

namespace RegistrationAndLoginApp.Services.BussinesLogicLayer
{
    /// <summary>
    /// Business Logic Layer for User operations
    /// </summary>
    public class UserLogic
    {

        

        //class level variables

        private IUserDAO _userDAO;

        /// <summary>
        /// Parameterized constructor 
        /// </summary>
        /// <param name="userDAO"></param>
        public UserLogic(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        /// <summary>
        /// Get all groups from the data storage
        /// </summary>
        /// <returns></returns>
        public List<GroupViewModel> GetAllGroups()
        {
            // Declare and initialize
            List<GroupDomainModel> domainGroups = _userDAO.GetAllGroups();
            List<GroupViewModel> viewGroups = new List<GroupViewModel>();

            foreach (GroupDomainModel domainGroup in domainGroups)
            {
                viewGroups.Add(GroupMapper.FromDomainModel(domainGroup));
            }

            return viewGroups;
        }

        /// <summary>
        /// Validates the given username and password against the stored users.
        /// Returns a tuple indicating whether validation succeeded and the corresponding UserViewModel (nullable).
        /// </summary>
        /// <param name="username">The username entered by the user</param>
        /// <param name="password">The password entered by the user</param>
        /// <returns>Tuple: (isValidated, UserViewModel?)</returns>
        public (bool isValidated, UserViewModel? viewUser) ValidateUserCredentials(string username, string password)
        {
            UserDomainModel? domainUser;
            UserViewModel? viewUser = null;
            bool userExists = false, isValidated = false;

            // Call DAO
            (userExists, domainUser) = _userDAO.GetUserFromUsername(username);

            // Validate password if user exists
            if (userExists && domainUser != null && domainUser.Password == password)
            {
                viewUser = UserMapper.FromDomainModel(domainUser);
                isValidated = true;
            }

            return (isValidated, viewUser);
        }

        /// <summary>
        /// Add a new user from a UserViewModel
        /// </summary>
        /// <param name="viewUser"></param>
        /// <returns>
        /// Success: user's id
        /// Fail: -1
        /// </returns>
        public int AddUser(UserViewModel viewUser)
        {
            // Declare
            UserDomainModel domainUser;

            try
            {
                // Use the mapper to map the view model to a domain model
                domainUser = UserMapper.ToDomainModel(viewUser);
            }
            catch (ArgumentNullException)
            {
                // Return to show that the parameter was null
                return -1;
            }

            // Send the domain model to the DAO and return the result
            return _userDAO.AddUser(domainUser);
        }

        /// <summary>
        /// Checks if a username already exists in the system
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <returns>True if username exists, false otherwise</returns>
        public bool UsernameExists(string username)
        {
            // Null or empty check
            if (string.IsNullOrWhiteSpace(username))
            {
                return false; // Consider empty usernames as non-existing
            }

            // Use DAO to get user by username
            var (userExists, _) = _userDAO.GetUserFromUsername(username);

            return userExists;
        }

    }
}
