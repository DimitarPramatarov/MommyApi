﻿namespace MommyApi.Services
{
    using Microsoft.EntityFrameworkCore;
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data;
    using MommyApi.Data.Models;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Services.ActivityCounter;
    using MommyApi.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

            if(model == null)
            {
                return "Answer is not created!";
            }

            var alreadyAnswered = await CheckIfUserAlreadyAnswered(model.PostId);

            if(alreadyAnswered == true)
            {
                return "User already created answer!";
            }

            var answer = new Answer
            {
                Description = model.Text,
                PostId = model.PostId,
            };

            await this.dbContext.AddAsync(answer);
            await this.dbContext.SaveChangesAsync();
            await this.activityCounterService.AnswerCount();


            return "Answer is created";
        }


        public async Task<IEnumerable<AnswerResponseModel>> GetAnswers(int postId)
        {
            var answers = await this.dbContext.Answers
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.PostId == postId)
                .ToListAsync();


            IList<AnswerResponseModel> responeAnswers = new List<AnswerResponseModel>();

            foreach(var item in answers)
            {
                var answer = new AnswerResponseModel
                {
                    Text = item.Description,
                    Username = item.CreatedBy,
                    CreatedOn = item.CreatedOn
                };

                responeAnswers.Add(answer);
            }

            return responeAnswers;
        }

        private async Task<bool> CheckIfUserAlreadyAnswered(int postId)
        {
            var userName = this.currentUserService.GetUserName();

            var result = await this.dbContext.Answers
                .Where(x => x.PostId == postId)
                .Select(x => x.CreatedBy == userName)
                .ToListAsync();

            if(result.Count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAnswer(int answerId, string description)
        {
            var answer = await this.dbContext.Answers.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var userId =  this.currentUserService.GetUserName();

            if(userId != answer.CreatedBy)
            {
                return false;
            }

            answer.Description = description;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAnswer(int answerId)
        {
            var answer = await this.dbContext.Answers.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var userId = this.currentUserService.GetUserName();


            if (userId != answer.CreatedBy)
            {
                return false;
            }

            answer.IsDeleted = true;

            return true;

        }
    }
}
