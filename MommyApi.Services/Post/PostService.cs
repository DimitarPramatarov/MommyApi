namespace MommyApi.Services.Post
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MommyApi.AppInfrastructure.Services;
    using Data;
    using Models.RequestModels;
    using Models.ResponseModels;
    using ActivityCounter;
    using Interfaces;

    public class PostService : IPostService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IActivityCounterService activityCounterService;

        public PostService(MommyApiDbContext dbContext,
            ICurrentUserService currentUserService,
            IActivityCounterService activityCounterService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.activityCounterService = activityCounterService;
        }


        public async Task<string> CreatePost(CreatePost createPost)
        {
            var userId = this.currentUserService.GetId();

            var newPost = new Data.Models.Post
            {
                Title = createPost.Title,
                Description = createPost.Description,
                CreatedOn = DateTime.UtcNow.Date,
                UserId = userId
            };

           await this.dbContext.AddAsync(newPost);
           await this.dbContext.SaveChangesAsync();
           await this.activityCounterService.PostCount();


            return "Your post is created!";
        }

        public async Task<IEnumerable<PostResponseModel>> GetPosts()
        {

            var result = await dbContext.Posts
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User)
                .ToListAsync();

            IList<PostResponseModel> resultRes = new List<PostResponseModel>();

            foreach(var item in result)
            {
                var post = new PostResponseModel
                {
                    CreatedOn = item.CreatedOn.ToShortDateString(),
                    PostId = item.PostId,
                    Title = item.Title,
                    Username = item.User.UserName
                };

                resultRes.Add(post);
            }

            return resultRes;
        }

        public async Task<IEnumerable<PostResponseModel>> MyPosts()
        {
            var userId = this.currentUserService.GetId();

            var myPosts = await this.dbContext.Posts
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .ToListAsync();

            IList<PostResponseModel> result = new List<PostResponseModel>();

            foreach(var item in myPosts)
            {
                var myPost = new PostResponseModel
                {
                    CreatedOn = item.CreatedOn.ToShortDateString(),
                    PostId = item.PostId,
                    Title = item.Title,
                    Username = item.CreatedBy
                };

                result.Add(myPost);
            }

            return result;
        }

        public async Task<PostDetailsResponseModel> PostDetails(Guid postId)
        {
            var postDetails = await this.dbContext.Posts
                .Where(x => x.PostId == postId && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            var post = new PostDetailsResponseModel
            {
                IsAnswered = postDetails.Answered,
                Username = postDetails.CreatedBy,
                CreatedOn = postDetails.CreatedOn.ToShortDateString(),
                PostId = postDetails.PostId,
                Description = postDetails.Description,
                Title = postDetails.Title,
            };

            return post;
        }

        public async Task<bool> SetPostAsAnswered(Guid postId)
        {
            var userId = this.currentUserService.GetId();

            var post = await this.dbContext.Posts
                .Where(x => x.PostId == postId)
                .FirstOrDefaultAsync();
            
            if(userId != post.UserId)
            {
                return false;
            }
            
            post.Answered = true;

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            var userId = this.currentUserService.GetId();

            var post = await this.dbContext.Posts.Where(x => x.PostId == postId).FirstOrDefaultAsync();

            if(userId != post.UserId)
            {
                return false;
            }

            post.IsDeleted = true;
            post.DeletedOn = DateTime.UtcNow;
            post.DeletedBy = userId;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdatePost(Guid postId, string description)
        {
            var answer = await this.dbContext.Posts.Where(x => x.PostId == postId).FirstOrDefaultAsync();
            var userId = this.currentUserService.GetUserName();

            if (userId != answer.CreatedBy)
            {
                return false;
            }

            answer.Description = description;
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
