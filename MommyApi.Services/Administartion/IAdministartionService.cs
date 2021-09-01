namespace MommyApi.Services.Administartion
{
    using MommyApi.Models.RequestModels;
    using System.Threading.Tasks;

    public interface IAdministartionService
    {
        Task<bool> SoftDelete(int id);

        Task AddUserToRole(int userId, string role);

        Task<string> EditUserRole(int userId, string role);

        Task<string> UpdateUserProfile(UpdateProfileRequestModel requestModel);
        
        Task<string> EditPost(int id, string description);

        Task<string> EditAsnwer(int id, string description);

        Task<string> EditSubAnswer(int id, string description);
    }
}
