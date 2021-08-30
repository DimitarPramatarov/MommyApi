namespace MommyApi.Services.Interfaces
{
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task<string> CreateAnswer(AnswerRequestModel requestModel);

        Task<IEnumerable<AnswerResponseModel>> GetAnswers(int postId);

        Task<bool> UpdateAnswer(int postId, string description);
    }
}
