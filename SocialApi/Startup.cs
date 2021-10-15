using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SocialApi_Business.Interfaces;
using SocialApi_Business.Repositories;
using SocialApi_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApi
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
            //    services.AddCors((options) =>
            //    {
            //        options.AddPolicy(name: "dev", builder =>
            //        { 
            //        builder.WithOrigins(
            //            "http://localhost:4200",
            //            "https://localhost:5001",
            //            "https://localhost:44348",
            //            "http://localhost:5000"
            //            )
            //        .AllowAnyHeader()
            //        .AllowAnyMethod();
            //    });

            //});
            services.AddCors();

            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialApi", Version = "v1" });
            });
            services.AddDbContext<SocialDBContext>(options => {
                if (!options.IsConfigured)
                {
                    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SocialDB;Trusted_Connection=True;");
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
