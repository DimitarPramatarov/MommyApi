namespace MommyApi.Data.Models
{
    using MommyApi.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class Post : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public bool Answered { get; set; } = false;



    }
}
