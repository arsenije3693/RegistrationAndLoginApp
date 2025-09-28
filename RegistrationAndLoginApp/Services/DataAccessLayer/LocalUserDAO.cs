using RegistrationAndLoginApp.Models.DomainModels;
using RegistrationAndLoginApp.Services.Interfaces;
using System.Collections.Generic;

namespace RegistrationAndLoginApp.Services.DataAccessLayer
{
    public class LocalUserDAO : IUserDAO
    {
        private List<UserDomainModel> _users;
        private List<GroupDomainModel> _groups;
        private int _nextId;

        public LocalUserDAO()
        {
            _users = new List<UserDomainModel>();
            _groups = new List<GroupDomainModel>();
            _nextId = 1;
            PopulateGroups();
        }

        public int AddUser(UserDomainModel user)
        {
            user.Id = _nextId;

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
            _users.Add(user);
            return user.Id;
        }

        public void PopulateGroups()
        {
            _groups.Add(new GroupDomainModel { Id = 1, Name = "Member" });
            _groups.Add(new GroupDomainModel { Id = 2, Name = "Admin" });
            _groups.Add(new GroupDomainModel { Id = 3, Name = "Developer" });
            _groups.Add(new GroupDomainModel { Id = 4, Name = "Guest" });
        }

        public List<GroupDomainModel> GetAllGroups()
        {
            return _groups;
        }

        // ✅ Implement missing interface member
        public List<GroupDomainModel> GetGroups()
        {
            return _groups;
        }

        // Optional: return all users for debugging
        public List<UserDomainModel> GetAllUsers()
        {
            return _users;
        }

      public  (bool wasUserFound, UserDomainModel? foundUser) GetUserFromUsername(string userUsername)
        {
            UserDomainModel? foundUser;

            foundUser = _users.FirstOrDefault(user => user.Username == userUsername, null);

            return (foundUser != null, foundUser);
        }

    }
}
