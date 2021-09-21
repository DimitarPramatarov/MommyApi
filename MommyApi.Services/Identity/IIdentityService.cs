namespace MommyApi.Services.Interfaces
{
    using MommyApi.Models.RequestModels;
    using System.Threading.Tasks;
    using MommyApi.Models.ResponseModels;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, string secret);

        Task<bool> Register(RegisterRequestModel reuqestModel);

        Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel);
    }
}
