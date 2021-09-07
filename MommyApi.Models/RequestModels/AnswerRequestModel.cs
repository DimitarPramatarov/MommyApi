using System;

namespace MommyApi.Models.RequestModels
{
    public class AnswerRequestModel
    {
        public string Text { get; set; }

        public Guid PostId { get; set; }

    }
}
