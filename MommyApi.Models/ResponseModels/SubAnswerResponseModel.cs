namespace MommyApi.Models.ResponseModels
{
    using System;

    public class SubAnswerResponseModel
    {
        public string CreatedBy { get; set; }

        public string Description { get; set; }

        public Guid SubAnswerId { get; set; }

        public string CreatedOn { get; set; }

    }
}
