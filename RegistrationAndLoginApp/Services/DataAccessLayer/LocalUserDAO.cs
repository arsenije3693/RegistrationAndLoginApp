/*
 * Arsenije Brajovic
 * CST-350
 * 9/17/2025
 * Register and Logic
 * Activity 2
 */
using RegistrationAndLoginApp.Models.DomainModels;
using RegistrationAndLoginApp.Services.Interfaces;
using System.Collections.Generic;

namespace RegistrationAndLoginApp.Services.DataAccessLayer
{
    public class LocalUserDAO : IUserDAO
    {
        // In-memory storage for users and groups
        private List<UserDomainModel> _users;
        private List<GroupDomainModel> _groups;
        private int _nextId;

        public LocalUserDAO()
        {
            _users = new List<UserDomainModel>();
            _groups = new List<GroupDomainModel>();
            _nextId = 1;
            PopulateGroups(); // Initialize default groups
        }

        // Add a new user and assign groups
        public int AddUser(UserDomainModel user)
        {
            user.Id = _nextId;

            // Replace group references with existing group objects
            for (int i = 0; i < user.Groups.Count; i++)
            {
                for (int j = 0; j < _groups.Count; j++)
                {
                    if (_groups[j].Id == user.Groups[i].Id)
                    {
                        user.Groups[i] = _groups[j];
                        break;
                    }
                }
            }

            _nextId++;
            _users.Add(user); // Store user in memory
            return user.Id;
        }

        // Preload some default user groups
        public void PopulateGroups()
        {
            _groups.Add(new GroupDomainModel { Id = 1, Name = "Member" });
            _groups.Add(new GroupDomainModel { Id = 2, Name = "Admin" });
            _groups.Add(new GroupDomainModel { Id = 3, Name = "Developer" });
            _groups.Add(new GroupDomainModel { Id = 4, Name = "Guest" });
        }

        // Return all groups
        public List<GroupDomainModel> GetAllGroups()
        {
            return _groups;
        }

        // Same as GetAllGroups, required by interface
        public List<GroupDomainModel> GetGroups()
        {
            return _groups;
        }

        // Return all users (for testing/debugging)
        public List<UserDomainModel> GetAllUsers()
        {
            return _users;
        }

        // Find a user by username
        public (bool wasUserFound, UserDomainModel? foundUser) GetUserFromUsername(string userUsername)
        {
            UserDomainModel? foundUser;

            foundUser = _users.FirstOrDefault(user => user.Username == userUsername, null);

            return (foundUser != null, foundUser);
        }
    }
}
