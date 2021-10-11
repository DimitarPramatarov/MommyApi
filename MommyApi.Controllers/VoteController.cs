namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services.Votes;
    using System;
    using MommyApi.Models.RequestModels;

    public class VoteController : ApiController
    {
        private readonly IVoteService voteService;

        public VoteController(IVoteService voteService)
        {
            this.voteService = voteService;
        }


        [HttpPost]
        [Route(nameof(PlusVote))]
        public async Task<ActionResult> PlusVote(VoteRequestModel requestModel)
        {

            var result = await this.voteService.AddPlusVoteById(requestModel.Id);

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(MinusVote))]
        public async Task<ActionResult> MinusVote(VoteRequestModel requestModel)
        {
            var result = await this.voteService.AddMinusVoteById(requestModel.Id);

            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(GetTotalVotes))]
        public async Task<ActionResult> GetTotalVotes(Guid id)
        {
            var result = await this.voteService.GetTotalVotesById(id);

            if(id == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
