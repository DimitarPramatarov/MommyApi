namespace MommyApi.Services.Interfaces
{
    using MommyApi.Models.RequestModels;
    using System.Threading.Tasks;
    using MommyApi.Models.ResponseModels;

    public interface IIdentityService
    {
        Task<string> GenerateJwtToken(string userId, string username, string role, string secret);

        Task<bool> Register(RegisterRequestModel reuqestModel);

        Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel);
    }
}
