namespace MommyApi.Services.Profile
{
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data;
    using System.Threading.Tasks;

    public class ProfileService : IProfileService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public async Task<bool> CreateProfileByUser()
        {
            var userId = currentUserService.GetId();
            var username = currentUserService.GetUserName();


            var profile = new UserProfile
            {
                UserId = userId,
                Username = username,
            };

            if(profile == null)
            {
                return false;
            }

            await dbContext.AddAsync(profile);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
