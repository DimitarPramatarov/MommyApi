using System;

namespace MommyApi.Services.Profile
{
    using MommyApi.AppInfrastructure.Services;
    using Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models.ResponseModels;
    using Models.RequestModels;

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
                Asnwers = 0,
                Follows = 0,
                Posts = 0,
            };

            if(profile == null)
            {
                return false;
            }

            await this.dbContext.AddAsync(profile);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ProfileResponseModel> ProfileDetails(string username)
        {
            var profileDetails = await this.dbContext.UserProfiles.Where(x => x.Username == username).FirstOrDefaultAsync();

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

        public async Task<ProfileResponseModel> MyProfile()
        {
            var currentUserId = this.currentUserService.GetId();

            var myProfileDetails = await this.dbContext.UserProfiles.FindAsync(currentUserId);

            if (myProfileDetails is null)
            {
                return null;
            }

            var profile = new ProfileResponseModel
            {
                Answers = myProfileDetails.Asnwers,
                Follows = myProfileDetails.Follows,
                Description = myProfileDetails.Description,
                MainPhotoUrl = myProfileDetails.MainPhotoUrl,
                Posts = myProfileDetails.Posts,
                UserName = myProfileDetails.Username,
            };

            return profile;

        }

         
        public  async Task<bool> UpdateProfile(UpdateProfileRequestModel requestModel)
        {
            var currentUserId = this.currentUserService.GetId();

            var profile = await this.dbContext.UserProfiles
                .Where(x => x.UserId == requestModel.UserId).FirstOrDefaultAsync();

            if(currentUserId != requestModel.UserId)
            {
                return false;
            }

            if(requestModel.Descritpion != null)
            {
                profile.Description = requestModel.Descritpion;
            }

            if(requestModel.MainPhotoUrl != null)
            {
                profile.MainPhotoUrl = requestModel.Descritpion;
            }

            await this.dbContext.SaveChangesAsync();
             

            return true;
        }
    }
}
