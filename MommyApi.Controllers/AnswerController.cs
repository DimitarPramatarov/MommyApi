namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.RequestModels;
    using Models.ResponseModels;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
            if(requestModel.Text is null)
            {
                return BadRequest("Answer cannot be null");
            }

            var answer = await answerService.CreateAnswer(requestModel);

            return Ok(answer);
        }

        [HttpGet]
        [Route(nameof(GetAnswers))]
        public async Task<IEnumerable<AnswerResponseModel>> GetAnswers(int postId)
        =>  await this.answerService.GetAnswers(postId);

        
        [HttpPut]
        [Route(nameof(UpdateAnswer))]
        public async Task<ActionResult> UpdateAnswer(int answerId, string description)
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
        public async Task<ActionResult> DeleteAnswer(int answerId)
        {
            var result = await this.answerService.DeleteAnswer(answerId);

            if(result is false)
            {
                return BadRequest("This user cannot delete answer");
            }

            return Ok(true);
        }

        //TODO : Add SetCorrectAnswer

        [HttpPut]
        [Route(nameof(SetCorrectAnswer))]
        public async Task<ActionResult> SetCorrectAnswer(int answerId)
        {
           
            var result = await this.answerService.SetCorrectAnswer(answerId);

            return Ok(result);
        }
    }
}
