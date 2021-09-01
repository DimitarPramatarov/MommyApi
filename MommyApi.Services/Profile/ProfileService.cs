namespace MommyApi.Services.Profile
{
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Models.RequestModels;

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

        public async Task<ProfileResponseModel> ProfileDetails(string profileId)
        {
            var profileDetails = await this.dbContext.UserProfiles.FindAsync(profileId);

            if(profileDetails == null)
            {
                return null;
            }

            var profile = new ProfileResponseModel
            {
                Answers = profileDetails.Asnwers,
                Follows = profileDetails.Follows,
                Description = profileDetails.Description,
                MainPhotoUrl = profileDetails.MainPhotoUrl,
                Posts = profileDetails.Posts,
                UserName = profileDetails.Username,
            };

            return profile;
        }

         
        public  async Task<bool> UpdateProfile(UpdateProfileRequestModel requestModel)
        {
            var currentUserId = currentUserService.GetId();

            var profile = await this.dbContext.UserProfiles
                .Where(x => x.UserId == requestModel.UserId).FirstOrDefaultAsync();

            if(currentUserId != requestModel.UserId)
            {
                return false;
            }

            profile.Description = requestModel.Descritpion;
            profile.MainPhotoUrl = requestModel.Descritpion;

            await dbContext.SaveChangesAsync();
             

            return true;
        }
    }
}
