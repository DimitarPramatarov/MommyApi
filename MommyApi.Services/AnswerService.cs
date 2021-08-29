namespace MommyApi.Services
{
    using Microsoft.EntityFrameworkCore;
    using MommyApi.Data;
    using MommyApi.Data.Models;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<IEnumerable<AnswerResponseModel>> GetAnswers(string postId)
        {
            var answers = await this.dbContext.Answers
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.PostId == postId)
                .Include(x => x.Post)
                .ToListAsync();


            IList<AnswerResponseModel> responeAnswers = new List<AnswerResponseModel>();

            foreach(var item in answers)
            {
                var answer = new AnswerResponseModel
                {
                    Text = item.Text,
                    Username = item.CreatedBy,
                    CreatedOn = item.CreatedOn
                };

                responeAnswers.Add(answer);
            }

            return responeAnswers;
        }
    }
}
