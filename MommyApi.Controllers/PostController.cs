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

            if (result is null)
            {
                return BadRequest("Sorry, no posts");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(CreatePost))]
        public async Task<ActionResult> CreatePost(CreatePost createPost)
        {
            if (createPost is null)
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

            if (myPosts is null)
            {
                return BadRequest("No posts");
            }

            return Ok(myPosts);
        }

        [HttpGet]
        [Route(nameof(Details))]
        public async Task<ActionResult> Details(int id)
        {
            var postDetails = await postService.PostDetails(id);

            if (postDetails is null)
            {
                return BadRequest("Post not found");
            }

            return Ok(postDetails);
        }

        [HttpPost]
        [Route(nameof(SetIsAnswered))]
        public async Task<ActionResult> SetIsAnswered(int postId)
        {
            var result = await postService.SetPostAsAnswered(postId);

            if (result == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(DeletePost))]
        public async Task<ActionResult> DeletePost(int postId)
        {
            var result = await postService.DeletePost(postId);

            if(result is false)
            {
                return BadRequest("User cannot delete the post");
            }

            return Ok("Post is deleted");
            
        }


        [HttpPut]
        [Route(nameof(UpdatePost))]
        public async Task<ActionResult> UpdatePost(int postId, string description)
        {
            var result = await this.postService.UpdatePost(postId, description);

            if (result is false)
            {
                return BadRequest("This user cannot update the asnwer");
            }

            return Ok(result);
        }
    }
}
