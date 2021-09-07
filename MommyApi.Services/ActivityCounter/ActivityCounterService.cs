namespace MommyApi.Services.ActivityCounter
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using MommyApi.AppInfrastructure.Services;
    using Data;

    public class ActivityCounterService : IActivityCounterService
    {

        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public ActivityCounterService(MommyApiDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task PostCount()
        {
            var user = this.currentUserService.GetId();

            var totalPosts = await this.dbContext.UserProfiles
                .Where(x => x.UserId == user)
                .FirstOrDefaultAsync();

            totalPosts.Posts++;
            await this.dbContext.SaveChangesAsync();

        }

        public async Task AnswerCount()
        {
            var user = this.currentUserService.GetId();

            var totalAnswers = await this.dbContext.UserProfiles
                .Where(x => x.UserId == user)
                .FirstOrDefaultAsync();

            totalAnswers.Asnwers++;
            await this.dbContext.SaveChangesAsync();

        }
    }
}
