﻿namespace MommyApi.Services
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

            var result =  dbContext.Posts.ToList();

            IList<PostResponseModel> resultRes = new List<PostResponseModel>();


            foreach(var item in result)
            {
                var post = new PostResponseModel
                {
                    CreatedOn = item.CreatedOn,
                    PostId = item.Id,
                    Title = item.Title
                };

                resultRes.Add(post);
            }

            return resultRes;

        }
    }
}
