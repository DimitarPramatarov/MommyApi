using System.ComponentModel.DataAnnotations;

namespace MommyApi.Models.RequestModels
{
    public class SubAnswerRequestModel
    {
        [Required]
        public int AnswerId { get; set; }

        [Required]
        public string Descripton { get; set; }
    }
}
