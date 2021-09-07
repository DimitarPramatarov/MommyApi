namespace MommyApi.Services.Votes
{
    using System.Threading.Tasks;
    using System;

    public interface IVoteService
    {
        Task<string> AddPlusVoteById(Guid id);

        Task<string> AddMinusVoteById(Guid id);


        Task RemoveVoteById(Guid id);

        Task<int> GetTotalVotesById(Guid id);
    }
}
