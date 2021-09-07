namespace MommyApi.Services.Search
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Models.ResponseModels;
    using Models.RequestModels;

    public interface ISearchService
    {
         Task<IEnumerable<PostResponseModel>> Search(SearchRequestModel requestModel);

    }
}
