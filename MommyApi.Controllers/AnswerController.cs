namespace MommyApi.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MommyApi.Services.Answer;

    using Models.RequestModels;
    using Models.ResponseModels;


    public class AnswerController : ApiController
    {
        private readonly IAnswerService answerService;

        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }


        [HttpPost]
        [Route(nameof(CreateAnswer))]
        public async Task<ActionResult> CreateAnswer(AnswerRequestModel requestModel)
        {
                if(requestModel.Description is null)
            {
                return BadRequest("Answer cannot be null");
            }

            var answer = await answerService.CreateAnswer(requestModel);

            return Ok(answer);
        }

        [HttpGet]
        [Route(nameof(GetAnswers))]
        public async Task<IEnumerable<AnswerResponseModel>> GetAnswers(Guid postId)
        =>  await this.answerService.GetAnswers(postId);

        
        [HttpPut]
        [Route(nameof(UpdateAnswer))]
        public async Task<ActionResult> UpdateAnswer(Guid answerId, string description)
        {
            var result = await this.answerService.UpdateAnswer(answerId, description);

            if(result is false)
            {
                return BadRequest("This user cannot update the asnwer");
            }

            return Ok(true);
        }

        [HttpPost]
        [Route(nameof(DeleteAnswer))]
        public async Task<ActionResult<bool>> DeleteAnswer(ByIdRequestModel requestModel)
        {
            var result = await this.answerService.DeleteAnswer(requestModel.Id);

            if(result is false)
            {
                return BadRequest("This user cannot delete answer");
            }

            return Ok(true);
        }

        [HttpPut]
        [Route(nameof(SetCorrectAnswer))]
        public async Task<ActionResult<bool>> SetCorrectAnswer(SetCorrectAnswerRequestModel requestModel)
        {
           
            var result = await this.answerService.AcceptAnswer(requestModel.AnswerId);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
