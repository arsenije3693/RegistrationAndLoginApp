/*
 * Arsenije Brajovic
 * CST-350
 * 9/17/2025
 * Register and Logic App Group Domain Model
 * Activity 2 - Part 1
 */


namespace RegistrationAndLoginApp.Models.DomainModels
{
    public class GroupDomainModel
    {
        

        //class level properties
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public GroupDomainModel()
        {
            // Declare default values for the properties
            Id = 0;
            Name = string.Empty;
        }

    }
}
