namespace MommyApi.Services.Answer
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.RequestModels;
    using Models.ResponseModels;

    public interface IAnswerService
    {
        Task<string> CreateAnswer(AnswerRequestModel requestModel);

        Task<IEnumerable<AnswerResponseModel>> GetAnswers(Guid postId);

        Task<bool> UpdateAnswer(Guid answerId, string description);

        Task<bool> DeleteAnswer(Guid answerId);

        Task<string> AcceptAnswer(Guid asnwerId);
    }
}
