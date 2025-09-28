/*
 * Arsenije Brajovic
 * CST - 350
 * 9/19/2025
 * Registration and Login - User Mapper
 * Activity 2 - Part 1
 */

using System;
using System.Collections.Generic;
using RegistrationAndLoginApp.Models.DomainModels;
using RegistrationAndLoginApp.Models.ViewModels;

namespace RegistrationAndLoginApp.Services.Mappers
{
    //public - the class can be accessed from other assemblies
    // static - the class cannot be instantiated and can only contain static members
    public static class UserMapper
    {
        /// <summary>
        /// Map a user view model to a user domain model
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <summary>
        /// Map a user view model to a user domain model
        /// </summary>
        /// <param name="viewModel">User view model</param>
        /// <returns>User domain model</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static UserDomainModel ToDomainModel(UserViewModel viewModel)
        {
            // Declare and initialize
            UserDomainModel domainModel;
            List<GroupDomainModel> domainGroups = new List<GroupDomainModel>();

            // Check if the view model is null
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            // Map the view models to domain groups
            foreach (GroupViewModel viewGroup in viewModel.GroupPossibilities)
            {
                // Check if the user selected the current group
                if (viewGroup.IsSelected)
                {
                    // If yes, add the group to domain list
                    domainGroups.Add(new GroupDomainModel
                    {
                        Id = viewGroup.Id,
                        Name = viewGroup.Name
                    });
                }
            }

            // Create a user domain model based on the viewModel
            domainModel = new UserDomainModel
            {
                Id = viewModel.Id,
                Username = viewModel.Username,
                Password = viewModel.Password,
                Groups = domainGroups
            };

            // Return the mapped domain model
            return domainModel;
        }

        public static UserViewModel FromDomainModel(UserDomainModel domainUser)
        {
            // Declare and initialize
            GroupViewModel viewGroup;
            UserViewModel viewUser = new UserViewModel
            {
                // Map the id, username, and password
                Id = domainUser.Id,
                Username = domainUser.Username,
                Password = domainUser.Password
            };

            foreach (GroupDomainModel domainGroup in domainUser.Groups)
            {
                // Get the view group from the GroupMapper
                viewGroup = GroupMapper.FromDomainModel(domainGroup);

                // Set the IsSelected property to true
                viewGroup.IsSelected = true;

                // Add the view group to the Possibilities list
                viewUser.GroupPossibilities.Add(viewGroup);
            }

            // Return the viewUser
            return viewUser;
        }

    }
}

