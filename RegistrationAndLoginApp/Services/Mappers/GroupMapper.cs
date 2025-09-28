using RegistrationAndLoginApp.Models.DomainModels;
using RegistrationAndLoginApp.Models.ViewModels;

namespace RegistrationAndLoginApp.Services.Mappers
{
    /// <summary>
    /// mapper class for grp models
    /// </summary>
    public static class GroupMapper
    {
        /// <summary>
        /// Map a group model from a domain model to a view model
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        public static GroupViewModel FromDomainModel(GroupDomainModel domainModel)
        {
            // Use the domain model to map a new view model
            return new GroupViewModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                IsSelected = false
            };
        }
    }
}
