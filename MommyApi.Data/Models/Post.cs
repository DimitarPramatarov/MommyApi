namespace MommyApi.Data.Models
{
    using MommyApi.Data.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post : IDeletableEntity
    {
        public int PostId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public bool Answered { get; set; } = false;

        public IEnumerable<Answer> Answers { get; } = new HashSet<Answer>();

        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
