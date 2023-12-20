using GenericRepository.AppSettings;
using GenericRepository.Domain;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Infrastructure.Context
{
    public class ProjectDbContext : DbContext
    {
        private readonly AppSetting _appSetting;

        public ProjectDbContext(AppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseNpgsql(_appSetting.PostgresqlSettings.ConnectionString));
        }

        public DbSet<User> Users { get; set; }
    }
}