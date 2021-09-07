namespace MommyApi.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Data;
    using System;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
                c.RoutePrefix = string.Empty;
            });


        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<MommyApiDbContext>();

            dbContext.Database.Migrate();
        }

        public static void RoleSeeder(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<MommyApiDbContext>();

           
        }
    }
}
