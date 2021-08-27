namespace MommyApi.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MommyApi.Data.Models;

    public class MommyApiDbContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; init; }

        public MommyApiDbContext(DbContextOptions<MommyApiDbContext> options)
            : base(options)
        {
        }
    }
}
