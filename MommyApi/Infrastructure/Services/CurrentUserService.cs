namespace MommyApi.Infrastructure.Services
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => this.user = httpContextAccessor.HttpContext?.User;

        public string GetId()
            => this.user?.FindFirst(ClaimTypes.NameIdentifier).Value;

        public string GetUsername()
            => this.user?.Identity.Name;

    }
}
