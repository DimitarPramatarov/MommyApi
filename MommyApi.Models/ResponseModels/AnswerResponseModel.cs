namespace MommyApi.Models.ResponseModels
{
    using System;

    public class AnswerResponseModel
    {
        public string Username { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
