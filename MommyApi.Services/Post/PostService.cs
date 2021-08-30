namespace MommyApi.Services
{
    using System.Threading.Tasks;
    using MommyApi.Services.Interfaces;
    using MommyApi.Data;
    using MommyApi.Data.Models;
    using MommyApi.Models.RequestModels;
    using System;
    using System.Linq;
    using MommyApi.Models.ResponseModels;
    using System.Collections.Generic;
    using MommyApi.AppInfrastructure.Services;
    using Microsoft.EntityFrameworkCore;

    public class PostService : IPostService
    {
        private readonly MommyApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public PostService(MommyApiDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }


        public async Task<string> CreatePost(CreatePost createPost)
        {
            var userId = currentUserService.GetId();

            var newPost = new Post
            {
                Title = createPost.Title,
                Description = createPost.Description,
                CreatedOn = DateTime.UtcNow,
                UserId = userId
            };

           await dbContext.AddAsync(newPost);
           await dbContext.SaveChangesAsync();


            return "Your post is created!";
        }

        public async Task<IEnumerable<PostResponseModel>> GetPosts()
        {

            var result = await dbContext.Posts
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User)
                .ToListAsync();

            IList<PostResponseModel> resultRes = new List<PostResponseModel>();

            foreach(var item in result)
            {
                var post = new PostResponseModel
                {
                    CreatedOn = item.CreatedOn,
                    PostId = item.Id,
                    Title = item.Title,
                    Username = item.User.UserName
                };

                resultRes.Add(post);
            }

            return resultRes;
        }

        public async Task<IEnumerable<PostResponseModel>> MyPosts()
        {
            var userId = currentUserService.GetId();

            var myPosts = await dbContext.Posts
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .ToListAsync();

            IList<PostResponseModel> result = new List<PostResponseModel>();

            foreach(var item in myPosts)
            {
                var myPost = new PostResponseModel
                {
                    CreatedOn = item.CreatedOn,
                    PostId = item.Id,
                    Title = item.Title,
                    Username = item.CreatedBy
                };

                result.Add(myPost);
            }

            return result;
        }

        public async Task<PostDetailsResponseModel> PostDetails(int postId)
        {
            var postDetails = await this.dbContext.Posts
                .Where(x => x.Id == postId && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            var post = new PostDetailsResponseModel
            {
                IsAnswered = postDetails.Answered,
                Username = postDetails.CreatedBy,
                CreatedOn = postDetails.CreatedOn,
                PostId = postDetails.Id,
                Description = postDetails.Description,
                Title = postDetails.Title,
            };

            return post;
        }

        public async Task<bool> SetPostAsAnswered(int postId)
        {
            var userId = currentUserService.GetId();

            var post = await this.dbContext.Posts
                .Where(x => x.Id == postId)
                .FirstOrDefaultAsync();
            
            if(userId != post.UserId)
            {
                return false;
            }
            
            post.Answered = true;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePost(int postId)
        {
            var userId = currentUserService.GetId();

            var post = await this.dbContext.Posts.Where(x => x.Id == postId).FirstOrDefaultAsync();

            if(userId != post.UserId)
            {
                return false;
            }

            post.IsDeleted = true;

            return true;
        }

        public async Task<bool> UpdatePost(int postId, string description)
        {
            var answer = await this.dbContext.Posts.Where(x => x.PostId == postId).FirstOrDefaultAsync();
            var userId = currentUserService.GetUserName();

            if (userId != answer.CreatedBy)
            {
                return false;
            }

            answer.Description = description;
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
