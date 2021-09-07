namespace MommyApi.Services.SubAsnwer
{
    using Models.RequestModels;
    using Models.ResponseModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;


    public interface ISubAnswerService
    {
        Task<string> CreateSubAnswer(SubAnswerRequestModel requestModel);

        Task<IEnumerable<SubAnswerResponseModel>> GetSubAnswers(Guid postId);

        Task<bool> UpdateSubAnswer(Guid subAnswerId, string description);

        Task<bool> DeleteSubAnswer(Guid subAsnwerId);
    }
}
