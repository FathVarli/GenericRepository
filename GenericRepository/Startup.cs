using GenericRepository.AppSettings;
using GenericRepository.Helper.Mapper;
using GenericRepository.Helper.Mapper.Mapster;
using GenericRepository.Infrastructure.Context;
using GenericRepository.Infrastructure.Repository.Abstract;
using GenericRepository.Infrastructure.Repository.Base;
using GenericRepository.Infrastructure.Repository.Concrete;
using GenericRepository.Infrastructure.UnitOfWork;
using GenericRepository.ServiceLayer.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GenericRepository

{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GenericRepository", Version = "v1" });
            });

            #region AppSettings Configuration

            services.Configure<AppSetting>(Configuration);
            var appSettings = Configuration.GetSection(nameof(AppSetting)).Get<AppSetting>();
            services.AddSingleton(appSettings);

            #endregion

            services.AddDbContext<ProjectDbContext>();
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMapperAdapter, MapsterAdapter>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GenericRepository v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}