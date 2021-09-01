using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MommyApi.Common;
using MommyApi.Data;
using MommyApi.Data.Models;
using MommyApi.Models.RequestModels;
using System.Linq;
using System.Threading.Tasks;



namespace MommyApi.Services.Administartion
{
    public class AdministrationService : IAdministartionService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly UserManager<User> userManager;

        public AdministrationService(MommyApiDbContext dbContext,
            UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        //TODO: Is there a way to refactor this ?? 
        public async Task AddUserToRole(int userId, string role)
        {
            var user = await this.dbContext.Users.FindAsync(userId);

            await this.userManager.AddToRoleAsync(user, role);
            await this.dbContext.SaveChangesAsync();

        }

        public async Task<string> EditUserRole(int userId, string role)
        {
            var user = await this.dbContext.Users.FindAsync(userId);

            if (user is null)
            {
                return GlobalConstants.UserNotFound;
            }

            await this.userManager.AddToRoleAsync(user, role);
            await this.dbContext.SaveChangesAsync();

            return $"User is edited in {role} role";
        }
       
        public Task<bool> SoftDelete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> UpdateUserProfile(UpdateProfileRequestModel requestModel)
        {
            var user = await this.dbContext.UserProfiles.FindAsync(requestModel.UserId);

            if(user == null)
            {
                return GlobalConstants.NotFound;
            }

            if(requestModel.Descritpion != null)
                   user.Description = requestModel.Descritpion;
            
            if(requestModel.MainPhotoUrl != null)
                   user.MainPhotoUrl= requestModel.MainPhotoUrl;

            await this.dbContext.SaveChangesAsync();

            return "Profile is update";
        }

        public async Task<string> EditPost(int id, string description)
        {
            var post = await this.dbContext.Posts.FindAsync(id);

            if(post == null)
            {
                return GlobalConstants.NotFound;
            }

            post.Description = description;
            await this.dbContext.SaveChangesAsync();

            return "Post is edited";
            
        }

        public async Task<string> EditAsnwer(int id, string description)
        {
            var post = await this.dbContext.Answers.FindAsync(id);

            if (post == null)
            {
                return GlobalConstants.NotFound;
            }

            post.Text = description;
            await this.dbContext.SaveChangesAsync();

            return "Answer is edited";
        }

        public async Task<string> EditSubAnswer(int id, string description)
        {
            var post = await this.dbContext.SubAnswers.FindAsync(id);

            if (post == null)
            {
                return GlobalConstants.NotFound;
            }

            post.Description = description;
            await this.dbContext.SaveChangesAsync();

            return "SubAnswer is edited";
        }
    }
}
