
namespace MommyApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Models.RequestModels;
    using Models.ResponseModels;
    using Services.Search;

    public class SearchController : ApiController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        [Route(nameof(Search))]
        public async Task<ActionResult<PostResponseModel>> Search(SearchRequestModel requestModel)
        {
            var result = await this.searchService.Search(requestModel);

            if (result == null)
            {
                return BadRequest("nothing found");
            }

            return Ok(result);
        }
    }
}
