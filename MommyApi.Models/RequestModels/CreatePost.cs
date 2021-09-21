namespace MommyApi.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePost
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
