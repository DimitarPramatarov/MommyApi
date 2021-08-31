namespace MommyApi.Services.Profile
{
    using System.Threading.Tasks;

    public interface IProfileService
    {
        Task<bool> CreateProfileByUser(string username, string userId);

        Task<bool> UpdateProfile(string description, string mainPhotoUrl);
    }
}
