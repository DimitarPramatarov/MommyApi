namespace MommyApi.Services.SubAsnwer
{
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubAnswerService
    {
        Task<string> CreateSubAnswer(SubAnswerRequestModel requestModel);

        Task<IEnumerable<SubAnswerResponseModel>> GetSubAnswers(int postId);

        Task<bool> UpdateSubAnswer(int subAnswerId, string description);

        Task<bool> DeleteSubAnswer(int subAsnwerId);
    }
}
