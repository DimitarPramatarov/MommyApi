namespace MommyApi.Services.Administartion
{
    using MommyApi.Models.RequestModels;
    using System.Threading.Tasks;
    using System;


    public interface IAdministartionService
    {
        Task<bool> SoftDelete(Guid id);

        Task AddUserToRole(Guid userId, string role);

        Task<string> EditUserRole(Guid userId, string role);

        Task<string> UpdateUserProfile(UpdateProfileRequestModel requestModel);
        
        Task<string> EditPost(Guid id, string description);

        Task<string> EditAsnwer(Guid id, string description);

        Task<string> EditSubAnswer(Guid id, string description);
    }
}
