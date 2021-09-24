namespace MommyApi.Models.ResponseModels
{
    using System;

    public class AnswerResponseModel
    {
        public Guid AnswerId { get; set; }

        public string Username { get; set; }

        public string Text { get; set; }

        public string CreatedOn { get; set; }

        public bool IsCorrectAnswer { get; set; }
    }
}
