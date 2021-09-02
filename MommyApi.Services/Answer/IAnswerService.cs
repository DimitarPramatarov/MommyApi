namespace MommyApi.Services.Interfaces
{
    using Models.RequestModels;
    using Models.ResponseModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task<string> CreateAnswer(AnswerRequestModel requestModel);

        Task<IEnumerable<AnswerResponseModel>> GetAnswers(int postId);

        Task<bool> UpdateAnswer(int answerId, string description);

        Task<bool> DeleteAnswer(int answerId);

        Task<string> SetCorrectAnswer(int asnwerId);
    }
}
