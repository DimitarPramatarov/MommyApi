namespace MommyApi.AppInfrastructure.Services
{
    using System;

    public interface ICurrentUserService
    {
        string GetUserName();

        string GetId();
    }
}
