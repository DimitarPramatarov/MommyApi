namespace MommyApi.Models.ResponseModels
{
    using System;

    public class PostResponseModel
    {
        public Guid PostId { get; set; }

        public string Title { get; set; }

        public string CreatedOn { get; set; }

        public string Username { get; set; }

        public bool IsAnswered { get; set; }
    }
}
