namespace MommyApi.Services.Interfaces
{
    using MommyApi.Models.RequestModels;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, string secret);

        Task<bool> Register(RegisterRequestModel reuqestModel);

        Task<string> Login(LoginRequestModel loginRequestModel);
    }
}
