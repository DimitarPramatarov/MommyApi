namespace MommyApi.Services.Interfaces
{
    using MommyApi.Models.RequestModels;
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task<string> CreateAnswer(AnswerRequestModel requestModel);
    }
}
