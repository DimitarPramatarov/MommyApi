namespace MommyApi.Models.ResponseModels
{
    using System;

    public class PostResponseModel
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Username { get; set; }
    }
}
