namespace MommyApi.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class SearchRequestModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum 20 letters")]
        public string Description { get; set; }
    }
}
