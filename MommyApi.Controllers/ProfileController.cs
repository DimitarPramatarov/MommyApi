namespace MommyApi.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
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
        public async Task<ActionResult<ProfileResponseModel>> ProfileDetails(string username)
        {
            var result = await this.profileService.ProfileDetails(username);

            if(result is null)
            {
                return BadRequest("User profile not found");
            }


            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(MyProfile))]
        public async Task<ActionResult<ProfileResponseModel>> MyProfile()
        {
            var result = await this.profileService.MyProfile();

            if (result is null)
            {
                return BadRequest("Profile not found");
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
