namespace MommyApi.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Services.SubAsnwer;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SubAnswerController : ApiController
    {
        private readonly ISubAnswerService subAnswerService;

        public SubAnswerController(ISubAnswerService subAnswerService)
        {
            this.subAnswerService = subAnswerService;
        }


        [HttpPost]
        [Route(nameof(CreateSubAnswer))]
        public async Task<ActionResult> CreateSubAnswer(SubAnswerRequestModel requestModel)
        {
            if (requestModel.Description is null)
            {
                return BadRequest("Answer cannot be null");
            }

            var subAnswer = await subAnswerService.CreateSubAnswer(requestModel);

            return Ok(subAnswer);
        }

        [HttpGet]
        [Route(nameof(GetSubAnswers))]
        public async Task<IEnumerable<SubAnswerResponseModel>> GetSubAnswers(Guid answerId)
        => await this.subAnswerService.GetSubAnswers(answerId);


        [HttpPut]
        [Route(nameof(UpdateSubAnswer))]
        public async Task<ActionResult> UpdateSubAnswer(EditRequestModel requestModel)
        {
            var result = await this.subAnswerService.UpdateSubAnswer(requestModel.Id, requestModel.Description);

            if (result is false)
            {
                return BadRequest("This user cannot update the asnwer");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(DeleteSubAnswer))]
        public async Task<ActionResult> DeleteSubAnswer(ByIdRequestModel subAnswerId)
        {
            var result = await this.subAnswerService.DeleteSubAnswer(subAnswerId.Id);

            if (result is false)
            {
                return BadRequest("This user cannot delete answer");
            }

            return Ok(result);
        }
    }
}
