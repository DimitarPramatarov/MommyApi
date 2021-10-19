namespace MommyApi.Models.RequestModels
{
    using System;

    public class SubAnswerRequestModel
    {
        public string Description { get; set; }

        public Guid AnswerId { get; set; }
    }
}
