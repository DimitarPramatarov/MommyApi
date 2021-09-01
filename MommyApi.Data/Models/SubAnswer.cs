namespace MommyApi.Data.Models
{
    using MommyApi.Data.Models.Base;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SubAnswer : IDeletableEntity
    {
        public int SubAnswerId { get; set; }

        [Required]
        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

        public string Description { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime CreatedOn { get; set ; }
        
        public string CreatedBy { get ; set; }
        
        public DateTime? ModifedOn { get; set; }
        
        public string ModifiedBy { get; set; }
    }
}
