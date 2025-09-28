/*
 * Arsenije Brajovic
 * CST-350
 * 9/17/2025
 * Register and Logic - Interface
 * Activity 2 - Part 1
 */
using System.Collections.Generic;
using RegistrationAndLoginApp.Models.DomainModels;




namespace RegistrationAndLoginApp.Services.Interfaces
{
    public interface IUserDAO
    {
        /// <summary>
        /// add a new user to the data storage and return the new user's id
        /// this is called a method stub
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int AddUser(UserDomainModel user);
        List<GroupDomainModel> GetAllGroups();
        
        /// <summary>
        /// get  a user domain mdoel based on given username 
        /// </summary>
   


        /// get a list of groups from the data storage
        List<GroupDomainModel>GetGroups();


        (bool wasUserFound, UserDomainModel? foundUser) GetUserFromUsername(string userUsername);



    }

}  

 









