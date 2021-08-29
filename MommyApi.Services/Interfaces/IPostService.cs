namespace MommyApi.Services.Interfaces
{
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostService
    {
        Task<string> CreatePost(CreatePost createPost);

        Task<IEnumerable<PostResponseModel>> GetPosts();

        Task<IEnumerable<PostResponseModel>> MyPosts();
    }
}
