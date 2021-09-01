
namespace MommyApi.Test.Controllers
{
    using MommyApi.Controllers;
    using MommyApi.Services.Interfaces;
    using Xunit;

    public class PostControllerTest
    {

        private readonly IPostService postService;

        public PostControllerTest(IPostService postService)
        {
            this.postService = postService;
        }

        [Fact]
        public void PostShouldReturnNewPost()
        {
            // arrange

            var postController = new PostController(postService);

            //act 

            //assert
        }
    }
}
