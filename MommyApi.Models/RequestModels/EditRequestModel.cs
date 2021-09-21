namespace MommyApi.Models.RequestModels
{
    using System;

    public class EditRequestModel
    {
        public Guid PostId { get; set; }

        public string Description { get; set; }
    }
}
