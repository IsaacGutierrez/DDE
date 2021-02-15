using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dde.api.Helpers;
using dde.api.Services;
using dde.dataaccess.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection;

namespace dde.api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            //configure strongly typed setting object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
     
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddDbContext<DDEContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DDE.QA")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
