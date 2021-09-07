using System;
using MommyApi.Data.Models.Base;
using MommyApi.Data.Models.Enums;

namespace MommyApi.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote : IDeletableEntity
    {
        public Guid VoteId { get; set; } = new Guid();
       
        [Required]
        public VoteTypes VoteType { get; set; }

        [Required]
        public Guid VoteForId { get; set; }


        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
