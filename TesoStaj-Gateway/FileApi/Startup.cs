using FileApi.DatabaseSettings;
using FileApi.Middlewares;
using FileApi.Models;
using FileApi.Repository;
using FileApi.Repository.Interfaces;
using FileApi.Services;
using FileApi.Services.Interfaces;
using FileApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FileApi
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
            services.Configure<FileDatabaseSettings>(
                Configuration.GetSection(nameof(FileDatabaseSettings)));
            services.Configure<FileStorageSettings>(
                Configuration.GetSection(nameof(FileStorageSettings)));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<FileDatabaseSettings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<FileStorageSettings>>().Value);

            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddSingleton<IStorageServices, StorageServices>();
            services.AddSingleton<IFileServices, FileServices>();
            
            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<IFormFile>, FileValidator>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}