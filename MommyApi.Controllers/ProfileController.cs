namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Models.RequestModels;
    using MommyApi.Services.Profile;
    using System.Threading.Tasks;

    public class ProfileController : ApiController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        [Route(nameof(ProfileDetails))]
        public async Task<ActionResult> ProfileDetails(string profileId)
        {
            var result = this.profileService.ProfileDetails(profileId);

            if(result == null)
            {
                return BadRequest("User profile not found");
            }


            return Ok(result);
        }

        [HttpPut]
        [Route(nameof(UpdateProfile))]
        public async Task<ActionResult> UpdateProfile(UpdateProfileRequestModel requestModel)
        {
            var result = await this.profileService.UpdateProfile(requestModel);

            if(result is false)
            {
                return BadRequest("Profile is not updated");
            }

            return Ok("Your profile is updated");
        }
    }
}
