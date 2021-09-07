namespace MommyApi.Services.Votes
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using MommyApi.AppInfrastructure.Services;
    using Data;
    using MommyApi.Data.Models;
    using MommyApi.Data.Models.Enums;
    using System.Threading.Tasks;

    public class VoteService : IVoteService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUser;

        public VoteService(MommyApiDbContext dbContext,
            ICurrentUserService currentUser)
        {
            this.dbContext = dbContext;
            this.currentUser = currentUser;
        }

        public async Task<string> AddPlusVoteById(Guid id)
        {

            var isVoted = await IsVoted(id);

            if (isVoted)
            {
                return "You already voted!";
            }

            var newVote = new Vote
            {
                VoteForId = id,
                VoteType = VoteTypes.PlusVote,
            };

            await this.dbContext.AddAsync(newVote);
            await this.dbContext.SaveChangesAsync();

            return "Thanks for your vote!";
        }

        public async Task<string> AddMinusVoteById(Guid id)
        {

            var isVoted = await IsVoted(id);

            if (isVoted)
            {
                return "You already voted!";
            }

            var newVote = new Vote
            {
                VoteForId = id,
                VoteType = VoteTypes.MinusVote,
            };

            await this.dbContext.AddAsync(newVote);
            await this.dbContext.SaveChangesAsync();

            return "Thanks for your vote"!;
        }

        public Task RemoveVoteById(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> GetTotalVotesById(Guid id)
        {
            var plusVotes = await this.dbContext.Votes.Where(x => x.VoteForId == id)
                .Where(x => x.VoteType == VoteTypes.PlusVote)
                .CountAsync();

            var minusVotes = await this.dbContext.Votes
                .Where(x => x.VoteForId == id)
                .Where(x => x.VoteType == VoteTypes.MinusVote)
                .CountAsync();

            var result = plusVotes - minusVotes;

            return result;
        }

        private async Task<bool> IsVoted(Guid id)
        {
            var user = this.currentUser.GetUserName();

            var result = await this.dbContext.Votes
                .Where(x => x.VoteForId == id)
                .Select(x => x.CreatedBy == user)
                .FirstOrDefaultAsync();

            return result;
        }
        
    }
}
