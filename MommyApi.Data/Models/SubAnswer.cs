namespace MommyApi.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SubAnswer : BasePublish
    {
        public int SubAnswerId { get; set; }

        [Required]
        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

    }
}
