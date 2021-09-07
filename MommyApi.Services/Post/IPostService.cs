namespace MommyApi.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.RequestModels;
    using Models.ResponseModels;

    public interface IPostService
    {
        Task<string> CreatePost(CreatePost createPost);

        Task<IEnumerable<PostResponseModel>> GetPosts();

        Task<IEnumerable<PostResponseModel>> MyPosts();

        Task<PostDetailsResponseModel> PostDetails(Guid PostId);

        Task<bool> SetPostAsAnswered(Guid postId);

        Task<bool> DeletePost(Guid postId);

        Task<bool> UpdatePost(Guid postId, string description);
    }
}
