using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace MommyApi.Services.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using Data;
    using Models.RequestModels;
    using Models.ResponseModels;

    public class SearchService : ISearchService
    {
        private readonly MommyApiDbContext dbContext;

        public SearchService(MommyApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PostResponseModel>> Search(SearchRequestModel requestModel)
        {
            var searchInTitle = await SearchInTitle(requestModel);

            if (searchInTitle != null)
            {
                return searchInTitle;
            }

            var searchInDescription = await SearchInDescription(requestModel);

        return searchInDescription;
        }

        protected async Task<IEnumerable<PostResponseModel>> SearchInDescription(SearchRequestModel requestModel)
        {
            var result = await this.dbContext.Posts.Where(x => x.Description.Contains(requestModel.Description)).ToListAsync();

            IList<PostResponseModel> searchResult = new List<PostResponseModel>();

            foreach (var item in result)
            {
                var post = new PostResponseModel
                {
                    Title = item.Title,
                    PostId = item.PostId
                };

                searchResult.Add(post);
            }

            return searchResult;
        }

        protected async Task<IEnumerable<PostResponseModel>> SearchInTitle(SearchRequestModel requestModel)
        {
            var result = await this.dbContext.Posts.Where(x => x.Title.Contains(requestModel.Description)).ToListAsync();

            IList<PostResponseModel> searchResult = new List<PostResponseModel>();

            foreach (var item in result)
            {
                var post = new PostResponseModel
                {
                    Title = item.Title,
                    PostId = item.PostId
                };

                searchResult.Add(post);
            }

            return searchResult;
        }

    }
}
