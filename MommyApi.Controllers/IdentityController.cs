namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Services.Interfaces;
    using MommyApi.Services.Profile;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel requestModel)
        {
            var result = await identityService.Register(requestModel);

            if(result is false)
            {
                return BadRequest("Account is not created");
            }

            return Ok("Account is created");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel requestModel)
        {

            string token = await identityService.Login(requestModel);

            if (token == "")
            {
                return Unauthorized();
            }

            return new LoginResponseModel
            {
                Token = token
            };
        }
    }
}
