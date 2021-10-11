namespace MommyApi
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Infrastructure.Extensions;
    using System.Text.Json.Serialization;
    using Data;
    using Data.Seeding;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDatabase(this.Configuration)
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddIdentity()
                .AddApplicationServices()
                .AddControllers();
                
            

            services
                .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration));
          
            services
                .AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
              using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MommyApiDbContext>();
                var roleSeed = new RoleSeeder();
                    roleSeed.Seed(dbContext, serviceScope.ServiceProvider);
                    dbContext.SaveChanges();
               
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
                .UseSwaggerUI()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .ApplyMigrations();
                
       
        }
    }
}
