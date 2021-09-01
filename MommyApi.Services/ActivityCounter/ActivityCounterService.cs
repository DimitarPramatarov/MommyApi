namespace MommyApi.Services.ActivityCounter
{
    using Microsoft.EntityFrameworkCore;
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class ActivityCounterService : IActivityCounterService
    {

        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public ActivityCounterService(MommyApiDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        // TODO: try to refactor this code and remove multiple methods

        public async Task PostCount()
        {
            var user = currentUserService.GetId();

            var totalPosts = await this.dbContext.UserProfiles
                .Where(x => x.UserId == user)
                .Select(x => x.Posts)
                .FirstOrDefaultAsync();

            totalPosts++;
            await dbContext.SaveChangesAsync();

        }

        public async Task AnswerCount()
        {
            var user = currentUserService.GetId();

            var totalAnswers = await this.dbContext.UserProfiles
                .Where(x => x.UserId == user)
                .Select(x => x.Asnwers)
                .FirstOrDefaultAsync();

            totalAnswers++;
            await dbContext.SaveChangesAsync();

        }

        public async Task SubAnswerCount()
        {
            var user = currentUserService.GetId();

            var totalSubAsnwerCount = await this.dbContext.UserProfiles
                .Where(x => x.UserId == user)
                .Select(x => x.Asnwers)
                .FirstOrDefaultAsync();

            totalSubAsnwerCount++;
            await dbContext.SaveChangesAsync();

        }
    }
}
