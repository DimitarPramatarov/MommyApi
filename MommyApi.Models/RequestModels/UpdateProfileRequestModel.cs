namespace MommyApi.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;
    using System;

    public class UpdateProfileRequestModel
    {

        [Required]
        public string UserId { get; set; }

        public string Descritpion { get; set; }
        
        public string MainPhotoUrl { get; set; }
    }
}
