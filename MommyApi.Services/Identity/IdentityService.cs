namespace MommyApi.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using MommyApi.AppInfrastructure;
    using MommyApi.Data.Models;
    using MommyApi.Models.RequestModels;
    using MommyApi.Models.ResponseModels;
    using MommyApi.Services.Interfaces;
    using MommyApi.Services.Profile;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly AppSettings appSettings;
        private readonly IProfileService profileService;


        public IdentityService(UserManager<User> userManager, 
            IOptions<AppSettings> appSettings,
            IProfileService profileService)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
            this.profileService = profileService;
        }

         
        public  string GenerateJwtToken(string userId, string username, string role, string secret)
        {
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
              {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                    
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token =  tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel requestModel)
        {
            var token = "";
            var user = await this.userManager.FindByNameAsync(requestModel.Username);

            if(user is null)
            {
                return null;
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, requestModel.Password);

            if (!passwordValid)
            {
                return null;
            }

            var userRole = await userManager.GetRolesAsync(user);

            var role = userRole.FirstOrDefault();

                token =  GenerateJwtToken(
                user.Id,
                user.UserName,
                role,
                this.appSettings.Secret
                );

                var loginResponse = new LoginResponseModel
                {
                    Token = token,
                    UserName = user.UserName
                };

                return loginResponse;
        }

        public async Task<bool> Register(RegisterRequestModel requestModel)
        {
            var user = new User
            {
                Email = requestModel.Email,
                UserName = requestModel.Username
            };

            var result = await this.userManager.CreateAsync(user, requestModel.Password);

            if(!result.Succeeded)
            {
                return false;
            }

            await this.userManager.AddToRoleAsync(user, "User");

            var createUserProfile = await profileService.CreateProfileByUser(user.UserName, user.Id);

            if(createUserProfile is false)
            {
                return false;
            }

            return true;
        }
        
    }
}
