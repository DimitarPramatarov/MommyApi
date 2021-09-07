namespace MommyApi.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;


    public class SubAnswer : BasePublish
    {
        public Guid SubAnswerId { get; set; } = new Guid();

        [Required]
        public Guid AnswerId { get; set; }

        public Answer Answer { get; set; }

    }
}
