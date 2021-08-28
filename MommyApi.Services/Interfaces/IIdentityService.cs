namespace MommyApi.Services.Interfaces
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, string secret);
    }
}
