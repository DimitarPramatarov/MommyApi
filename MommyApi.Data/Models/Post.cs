namespace MommyApi.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post : BasePublish
    {
        public Guid PostId { get; set; } = new Guid();

        [Required]
        public string Title { get; set; }
       

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public bool Answered { get; set; } = false;

        public IEnumerable<Answer> Answers { get; } = new HashSet<Answer>();

    }
}
