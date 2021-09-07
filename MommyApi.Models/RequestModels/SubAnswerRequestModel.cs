namespace MommyApi.Models.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SubAnswerRequestModel
    {
        [Required]
        public Guid AnswerId { get; set; }

        [Required]
        public string Descripton { get; set; }
    }
}
