namespace MommyApi.Data
{
    using MommyApi.Data.Models.Base;
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class UserProfile : IEntity
    {
        [Required]
        [Key]
        public string UserId { get; set; }

        [Required]
        public string Username { get; set; }

        public string Description { get; set; }

        public int Asnwers { get; set; }

        public int Posts { get; set; }

        public int Follows { get; set; }

        public string MainPhotoUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
