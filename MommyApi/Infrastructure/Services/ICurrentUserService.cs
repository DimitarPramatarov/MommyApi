namespace MommyApi.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        string GetUsername();

        string GetId();
    }
}
