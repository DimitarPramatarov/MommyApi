
namespace MommyApi.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
