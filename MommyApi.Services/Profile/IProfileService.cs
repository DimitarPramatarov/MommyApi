namespace MommyApi.Services.Profile
{
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using System.Threading.Tasks;

    public interface IProfileService
    {
        Task<bool> CreateProfileByUser(string username, string userId);

        Task<bool> UpdateProfile(UpdateProfileRequestModel requestModel);

        Task<ProfileResponseModel> ProfileDetails(string profileId);
    }
}
