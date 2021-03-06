namespace MommyApi.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Base;
    using AppInfrastructure.Services;

    public class MommyApiDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService currentUserService;
        public MommyApiDbContext(DbContextOptions<MommyApiDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<Post> Posts { get; init; }

        public DbSet<Answer> Answers { get; init; }

        public DbSet<UserProfile> UserProfiles {get; init;}

        public DbSet<SubAnswer> SubAnswers { get; init; }

        public DbSet<Vote> Votes { get; init; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInformation();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .Entity<SubAnswer>()
                .HasQueryFilter(x => !x.IsDeleted)
                .HasOne(x => x.Answer)
                .WithMany(x => x.SubAnswers)
                .HasForeignKey(x => x.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Answer>()
                .HasQueryFilter(x => !x.IsDeleted)
                .HasOne(x => x.Post)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.
                Entity<Post>()
                .HasQueryFilter(c => !c.IsDeleted)
                .HasOne(c => c.User)
                .WithMany(c => c.Post)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInformation()
        {
            this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    var username = this.currentUserService.GetUserName();

                    if(entry.Entity is IDeletableEntity deletableEntity)
                    {
                        if(entry.State == EntityState.Deleted)
                        {
                            deletableEntity.DeletedOn = DateTime.UtcNow;
                            deletableEntity.DeletedBy = username;
                            deletableEntity.IsDeleted = true;

                            entry.State = EntityState.Modified;

                            return;
                        }
                    }

                    if (entry.Entity is IEntity entity)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedBy = username;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entity.ModifedOn = DateTime.UtcNow;
                            entity.ModifiedBy = username;
                        }
                    }
                });

        }
    }
}
