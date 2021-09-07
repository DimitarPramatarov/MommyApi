namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services.Votes;
    using System;


    public class VoteController : ApiController
    {
        private readonly IVoteService voteService;

        public VoteController(IVoteService voteService)
        {
            this.voteService = voteService;
        }


        [HttpPost]
        [Route(nameof(PlusVote))]
        public async Task<ActionResult> PlusVote(Guid id)
        {

            var result = await this.voteService.AddPlusVoteById(id);

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(MinusVote))]
        public async Task<ActionResult> MinusVote(Guid id)
        {
            var result = await this.voteService.AddMinusVoteById(id);

            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(GetTotalVotes))]
        public async Task<ActionResult> GetTotalVotes(Guid id)
        {
            var result = await this.voteService.GetTotalVotesById(id);
            return Ok(result);
        }
    }
}
