using System;
using FileWebApplication.DatabaseSettings;
using FileWebApplication.Middlewares;
using FileWebApplication.Repository;
using FileWebApplication.Repository.Interfaces;
using FileWebApplication.Services;
using FileWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FileWebApplication
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
            services.Configure<FwaDatabaseSettings>(
                Configuration.GetSection(nameof(FwaDatabaseSettings)));
            services.Configure<FwaStorageSettings>(
                Configuration.GetSection(nameof(FwaStorageSettings)));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<FwaDatabaseSettings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<FwaStorageSettings>>().Value);

            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddSingleton<IStorageServices, StorageServices>();
            services.AddSingleton<IFileServices, FileServices>();
            services.AddSingleton<IUserServices, UserServices>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}