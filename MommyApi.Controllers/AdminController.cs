namespace MommyApi.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.RequestModels;
    using Services.Administartion;
    using System.Threading.Tasks;

    public class AdminController : ApiController
    {
        private readonly IAdministartionService service;

        public AdminController(IAdministartionService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route(nameof(AddUserToRole))]
        public async Task<ActionResult> AddUserToRole(Guid userId, string role)
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
        public async Task<ActionResult> EditUserRole(Guid userId, string role)
        {
            if (role is null)
            {
                return BadRequest();
            }

            await this.service.EditUserRole(userId, role);

            return Ok("User is added to role");
        }

        [Authorize(Roles = "SUPERADMIN")]
        [HttpPut]
        [Route(nameof(EditPost))]
        public async Task<ActionResult> EditPost(Guid postId, string description)
        {
            if (description is null)
            {
                return BadRequest();
            }

            await this.service.EditPost(postId, description);

            return Ok("Post is edited");
        }

        [HttpPut]
        [Route(nameof(EditAnswer))]
        public async Task<ActionResult> EditAnswer(Guid userId, string description)
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
        public async Task<ActionResult> EditSubAnswer(Guid userId, string description)
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
