namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Data;
    using MommyApi.Data.Models;
    using MommyApi.Models.RequestModels;

    public class PostController : ApiController
    {
        private readonly MommyApiDbContext dbContext;

        public PostController(MommyApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Post()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult CreatePost(CreatePost createPost, Post post)
        {
            if(createPost is null)
            {
                return BadRequest("Title or description cannot be empty");
            }

            var newPost = new Post
            {
                Title = createPost.Title,
                Description = createPost.Title
            };

            dbContext.Add(newPost);
            dbContext.SaveChanges();


            return Ok();
        }
    }
}
