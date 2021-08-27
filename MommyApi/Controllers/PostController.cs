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
        public ActionResult GetAllPosts()
        {
            var result = postService.GetPosts();

            if(result is null)
            {
                return BadRequest("Sorry, no posts");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePost createPost)
        {
            if(createPost is null)
            {
                return BadRequest("Title or description cannot be empty");
            }


            var result = await postService.CreatePost(createPost);

            return Ok(result);
        }
    }
}
