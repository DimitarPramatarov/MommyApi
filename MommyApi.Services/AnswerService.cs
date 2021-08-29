namespace MommyApi.Services
{
    using MommyApi.Data;
    using MommyApi.Data.Models;
    using MommyApi.Models.RequestModels;
    using MommyApi.Services.Interfaces;
    using System.Threading.Tasks;

    public class AnswerService : IAnswerService
    {
        private readonly MommyApiDbContext dbContext;

        public AnswerService(MommyApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAnswer(AnswerRequestModel model)
        {

            if(model.PostId is null && model.Text is null)
            {
                return "Answer is not created!";
            }


            var answer = new Answer
            {
                Text = model.Text,
                PostId = model.PostId,
            };

            await dbContext.AddAsync(answer);
            await dbContext.SaveChangesAsync();


            return "Answer is created";

             
        } 
    }
}
