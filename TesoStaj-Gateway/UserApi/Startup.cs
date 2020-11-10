using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using UserApi.DatabaseSettings;
using UserApi.Middlewares;
using UserApi.Models;
using UserApi.Repository;
using UserApi.Repository.Interfaces;
using UserApi.Services;
using UserApi.Services.Interfaces;
using UserApi.Validators;

namespace UserApi
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
            services.Configure<UserDatabaseSettings>(
                Configuration.GetSection(nameof(UserDatabaseSettings)));
            
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);
            
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddSingleton<IUserServices, UserServices>();

            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<UserDto>, UserValidator>();
            
            
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