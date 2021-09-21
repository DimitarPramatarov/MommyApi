using System.ComponentModel.DataAnnotations;

namespace MommyApi.Models.RequestModels
{

    public class CreatePost
    {
        [MinLength(10)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MinLength(50)]
        [MaxLength(500)]
        public string Description { get; set; }

    }
}
