using MommyApi.Services.Search;

namespace MommyApi.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Text;

    using Data;
    using AppInfrastructure;
    using MommyApi.AppInfrastructure.Services;
    using MommyApi.Data.Models;
    using Services;
    using Services.ActivityCounter;
    using Services.Interfaces;
    using Services.Profile;
    using Services.SubAsnwer;
    using Services.Administartion;
    using Services.Answer;
    using Services.Votes;
    using Services.Post;



    public static class ServiceCollectionExtension
    {
        public static AppSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppSettings>();
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddDbContext<MommyApiDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                    options.ClaimsIdentity.RoleClaimType.IsNormalized();
                })
                .AddEntityFrameworkStores<MommyApiDbContext>();


            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
         this IServiceCollection services,
         AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
             .AddTransient<IIdentityService, IdentityService>()
              .AddTransient<ICurrentUserService, CurrentUserService>()
            .AddTransient<IPostService, PostService>()
            .AddTransient<IAnswerService, AnswerService>()
            .AddTransient<ISubAnswerService, SubAnswerService>()
            .AddTransient<IActivityCounterService, ActivityCounterService>()
             .AddTransient<IAdministartionService, AdministrationService>()
             .AddTransient<IVoteService, VoteService>()
             .AddTransient<ISearchService, SearchService>()
            .AddTransient<IProfileService, ProfileService>();

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {

                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert Jwt token",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                  {
                        new OpenApiSecurityScheme
                        {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                        },
                            new string[] { }
                  }
                });
            });
    }
}
