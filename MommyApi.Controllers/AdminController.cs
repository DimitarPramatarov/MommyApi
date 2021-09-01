namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MommyApi.Models.RequestModels;
    using MommyApi.Services.Administartion;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        private readonly IAdministartionService service;

        public AdminController(IAdministartionService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route(nameof(AddUserToRole))]
        public async Task<ActionResult> AddUserToRole(int userId, string role)
        {
            if(role is null)
            {
                return BadRequest();
            }

            await this.service.AddUserToRole(userId, role);

            return Ok("User is added to role");
        }

        [HttpPut]
        [Route(nameof(EditUserRole))]
        public async Task<ActionResult> EditUserRole(int userId, string role)
        {
            if (role is null)
            {
                return BadRequest();
            }

            await this.service.EditUserRole(userId, role);

            return Ok("User is added to role");
        }

        [HttpPut]
        [Route(nameof(EditPost))]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> EditPost(int userId, string description)
        {
            if (description is null)
            {
                return BadRequest();
            }

            await this.service.EditPost(userId, description);

            return Ok("Post is edited");
        }

        [HttpPut]
        [Route(nameof(EditAnswer))]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> EditAnswer(int userId, string description)
        {
            if (description is null)
            {
                return BadRequest();
            }

            await this.service.EditAsnwer(userId, description);

            return Ok("Answer is edited");
        }

        [HttpPut]
        [Route(nameof(EditSubAnswer))]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> EditSubAnswer(int userId, string description)
        {
            if (description is null)
            {
                return BadRequest();
            }

            await this.service.EditSubAnswer(userId, description);

            return Ok("SubAnswer is edited");
        }

        [HttpPut]
        [Route(nameof(UpdateUserProfile))]
        public async Task<ActionResult> UpdateUserProfile(UpdateProfileRequestModel requestModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await this.service.UpdateUserProfile(requestModel);

            return Ok(result);

            
        }
    }
}
