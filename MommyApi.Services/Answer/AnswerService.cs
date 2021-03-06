namespace MommyApi.Services.Answer
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data.Models;
    using Models.RequestModels;
    using Models.ResponseModels;
    using ActivityCounter;

    public class AnswerService : IAnswerService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IActivityCounterService activityCounterService;

        public AnswerService(MommyApiDbContext dbContext,
            ICurrentUserService currentUserService,
            IActivityCounterService activityCounterService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.activityCounterService = activityCounterService;
        }
        
        public async Task<string> CreateAnswer(AnswerRequestModel model)
        {

            if (model == null)
            {
                return "Answer is not created!";
            }

            var alreadyAnswered = await CheckIfUserAlreadyAnswered(model.PostId);

            if (alreadyAnswered)
            {
                return "User already created answer!";
            }

            var answer = new Answer
            {
                Description = model.Description,
                PostId = model.PostId,
            };

            await this.dbContext.AddAsync(answer);
            await this.dbContext.SaveChangesAsync();
            await this.activityCounterService.AnswerCount();


            return "Answer is created";
        }


        public async Task<IEnumerable<AnswerResponseModel>> GetAnswers(Guid postId)
        {
            var answers = await this.dbContext.Answers
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.PostId == postId)
                .ToListAsync();


            IList<AnswerResponseModel> responeAnswers = new List<AnswerResponseModel>();

            foreach (var item in answers)
            {
                var answer = new AnswerResponseModel
                {
                    AnswerId = item.AnswerId,
                    Text = item.Description,
                    Username = item.CreatedBy,
                    CreatedOn = item.CreatedOn.ToShortDateString(),
                    IsCorrectAnswer = item.CorrectAnswer
                };

                responeAnswers.Add(answer);
            }

            return responeAnswers;
        }

        private async Task<bool> CheckIfUserAlreadyAnswered(Guid postId)
        {
            var userName = this.currentUserService.GetUserName();

            var result = await this.dbContext.Answers
                .Where(x => x.PostId == postId)
                .Select(x => x.CreatedBy == userName)
                .ToListAsync();

            if (result.Count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAnswer(Guid answerId, string description)
        {
            var answer = await this.dbContext.Answers.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var userId = this.currentUserService.GetUserName();

            if (userId != answer.CreatedBy)
            {
                return false;
            }

            answer.Description = description;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAnswer(Guid answerId)
        {
            var answer = await this.dbContext.Answers.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var user = this.currentUserService.GetUserName();
            var userId = this.currentUserService.GetId();


            if (user != answer.CreatedBy)
            {
                return false;
            }

            answer.IsDeleted = true;
            answer.DeletedBy = userId;
            answer.DeletedOn = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<string> AcceptAnswer(Guid answerId)
        {
            var user = this.currentUserService.GetId();
            
            var answer = await this.dbContext.Answers.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();

            if (answer == null)
            {
                return "Not Found";
            }

            var postOwner = await this.dbContext.Posts.Where(x => x.PostId == answer.PostId).FirstOrDefaultAsync();

            if (postOwner.UserId != user)
            {
                return "Only post owner can accept answer!";
            }

            answer.CorrectAnswer = ChangeIsCorrect(answer.CorrectAnswer);
            postOwner.Answered = ChangeIsCorrect(postOwner.Answered);

            await this.dbContext.SaveChangesAsync();

            if (answer.CorrectAnswer == false)
            {
                return "You unaccept the answer";
            }

            return "You accept the answer";
        }

        private bool ChangeIsCorrect(bool answerValue)
        {
            if (answerValue == true)
            {
                return false;
            }
            
            return true;
        }
    }
}
