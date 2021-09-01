﻿namespace MommyApi.Data.Models
{
    using MommyApi.Data.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Answer : IDeletableEntity
    {
        public int AnswerId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        public bool CorrectAnswer { get; set; } = false;

        public IEnumerable<SubAnswer> SubAnswers { get; } = new HashSet<SubAnswer>();


        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get ; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
