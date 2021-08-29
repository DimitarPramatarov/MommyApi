namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using MommyApi.Services.Interfaces;
    using MommyApi.Models.RequestModels;

    public class PostController : ApiController
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        [Route(nameof(GetAllPosts))]
        public async Task<ActionResult> GetAllPosts()
        {
            var result = await postService.GetPosts();

            if(result is null)
            {
                return BadRequest("Sorry, no posts");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(CreatePost))]
        public async Task<ActionResult> CreatePost(CreatePost createPost)
        {
            if(createPost is null)
            {
                return BadRequest("Title or description cannot be empty");
            }


            var result = await postService.CreatePost(createPost);

            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(MyPosts))]
        public async Task<ActionResult> MyPosts()
        {
            var myPosts = await postService.MyPosts();

            if(myPosts is null)
            {
                return BadRequest("No posts");
            }

            return Ok(myPosts);
        }
    }
}
