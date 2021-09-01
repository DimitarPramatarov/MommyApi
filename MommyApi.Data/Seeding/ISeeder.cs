namespace MommyApi.Data.Seeding
{
    using System;

    public interface ISeeder
    {
        void Seed(MommyApiDbContext dbContext, IServiceProvider serviceProvider);
    }
}
