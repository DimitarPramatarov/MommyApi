namespace MommyApi.Models.ResponseModels
{
    public class ProfileResponseModel
    {
        public string UserName { get; set; }

        public int Follows { get; set; }

        public int Posts { get; set; }

        public int Answers { get; set; }

        public string Description { get; set; }

        public string MainPhotoUrl { get; set; }
    }
}
