using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.DatabaseSettings;
using Gateway.Middlewares;
using Gateway.Services;
using Gateway.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gateway
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
            services.Configure<ClientDatabaseSettings>(
                Configuration.GetSection(nameof(ClientDatabaseSettings)));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<ClientDatabaseSettings>>().Value);
            
            services.AddSingleton<IUserServices, UserServices>();
            services.AddSingleton<IFileServices, FileServices>();
            
            services.AddSession();
            services.AddMvc();
            
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
            
            app.UseSession();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}