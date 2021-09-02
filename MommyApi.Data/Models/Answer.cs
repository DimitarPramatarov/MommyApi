namespace MommyApi.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Answer : BasePublish
    {

        public int AnswerId { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        public bool CorrectAnswer { get; set; } = false;

        public IEnumerable<SubAnswer> SubAnswers { get; } = new HashSet<SubAnswer>();

    }
}
