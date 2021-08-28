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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.
                Entity<Post>()
                .HasQueryFilter(c => !c.IsDeleted)
                .HasOne(c => c.User)
                .WithMany(c => c.Post)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            base.OnModelCreating(builder);
        }
    }
}
