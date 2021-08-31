namespace MommyApi.Services.Profile
{
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class ProfileService : IProfileService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public ProfileService(MommyApiDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<bool> CreateProfileByUser(string username, string userId)
        {

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


        //To do create an update method for updating user profile // description / photo 
        public  async Task<bool> UpdateProfile(string userId, string description, string mainPhotoUrl)
        {
            var currentUserId = currentUserService.GetId();

            var profile = await this.dbContext.UserProfiles
                .Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if(currentUserId != userId)
            {
                return false;
            }

            profile.Description = description;
            profile.MainPhotoUrl = mainPhotoUrl;

            await dbContext.SaveChangesAsync();
             

            return true;
        }
    }
}
