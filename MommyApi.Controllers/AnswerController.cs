namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Services.Interfaces;
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


    }
}
