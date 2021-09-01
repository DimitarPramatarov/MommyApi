namespace MommyApi.Services.ActivityCounter
{
    using System.Threading.Tasks;

    public interface IActivityCounterService
    {
        Task PostCount();

        Task AnswerCount();

        Task SubAnswerCount();

    }
}
