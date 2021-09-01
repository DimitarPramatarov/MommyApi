﻿namespace MommyApi.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data;
    using MommyApi.Data.Models;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Services.SubAsnwer;

    public class SubAnswerService : ISubAnswerService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public SubAnswerService(MommyApiDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }
    
        public async Task<string> CreateSubAnswer(SubAnswerRequestModel requestModel)
        {

            if (requestModel == null)
            {
                return "Answer is not created!";
            }

       
            var answer = new SubAnswer
            {
                Description = requestModel.Descripton,
                AnswerId = requestModel.AnswerId,
                
            };

            await dbContext.AddAsync(answer);
            await dbContext.SaveChangesAsync();

            return "Answer is created";
        }


        public async Task<IEnumerable<SubAnswerResponseModel>> GetSubAnswers(int answerId)
        {
            var answers = await this.dbContext.SubAnswers
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.AnswerId == answerId)
                .ToListAsync();


            IList<SubAnswerResponseModel> responseSubAsnwer = new List<SubAnswerResponseModel>();

            foreach (var item in answers)
            {
                var answer = new SubAnswerResponseModel
                {
                    Description = item.Description,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = item.CreatedOn,
                    SubAnswerId = item.SubAnswerId,
                };

                responseSubAsnwer.Add(answer);
            }

            return responseSubAsnwer;
        }

        public async Task<bool> UpdateSubAnswer(int subAnswerId, string description)
        {
            var subAnswer = await this.dbContext.SubAnswers.Where(x => x.SubAnswerId == subAnswerId).FirstOrDefaultAsync();
            var userId = currentUserService.GetUserName();

            if (userId != subAnswer.CreatedBy)
            {
                return false;
            }

            subAnswer.Description = description;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSubAnswer(int subAnswerId)
        {
            var subAnswer = await this.dbContext.SubAnswers.Where(x => x.SubAnswerId == subAnswerId).FirstOrDefaultAsync();
            var userId = currentUserService.GetUserName();


            if (userId != subAnswer.CreatedBy)
            {
                return false;
            }

            subAnswer.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}
