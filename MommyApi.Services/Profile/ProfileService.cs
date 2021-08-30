namespace MommyApi.Services.Profile
{
    using Microsoft.EntityFrameworkCore;
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfileService : IProfileService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public async Task<bool> CreateProfileByUser(string username)
        {
            var userId = this.dbContext.Users
                .Where(x => x.UserName == username)
                .Select(x => x.Id)
                .FirstOrDefaultAsync()
                .ToString();

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
