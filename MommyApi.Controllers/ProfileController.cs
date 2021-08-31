namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Services.Profile;
    using System.Threading.Tasks;

    public class ProfileController : ApiController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpPut]
        [Route(nameof(UpdateProfile))]
        public async Task<ActionResult> UpdateProfile(string description, string mainPhotoUrl)
        {
            var result = await this.profileService.UpdateProfile(description, mainPhotoUrl);

            if(result is false)
            {
                return BadRequest("Profile is not updated");
            }

            return Ok("Your profile is updated");
        }
    }
}
