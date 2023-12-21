using GenericRepository.AppSettings;
using GenericRepository.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GenericRepository.Infrastructure.Context
{
    public class ProjectDbContext : DbContext
    {
        private readonly AppSetting _appSetting;

        public ProjectDbContext(AppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_appSetting.PostgresqlSettings.ConnectionString)
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
    }
}