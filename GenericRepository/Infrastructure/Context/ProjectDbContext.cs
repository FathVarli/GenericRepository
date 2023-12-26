using GenericRepository.AppSettings;
using GenericRepository.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GenericRepository.Infrastructure.Context
{
    public class ProjectDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly AppSetting _appSetting;

        public ProjectDbContext(AppSetting appSetting)
        {
            _appSetting = appSetting;
        }

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