/*
 * Arsenije Brajovic
 * CST-350
 * 9/17/2025
 * Register and Logic App Domain Model
 * Activity 2 - Part 1
 */

namespace RegistrationAndLoginApp.Models.DomainModels
{
    public class UserDomainModel
    {


        //class level properties
        public int Id { get; set; }


        public string Username { get; set; }

        public string Password { get; set; }

        public List<GroupDomainModel> Groups { get; set; } 

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
        }

    }
}
