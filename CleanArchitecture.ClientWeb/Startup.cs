using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Entity;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Core.Service;
using CleanArchitecture.Core.Logging;
using CleanArchitecture.Infrastructure.Logging;

namespace CleanArchitecture.ClientWeb
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
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseInMemoryDatabase(databaseName: "ApplicationDB"));

            // Services
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();

            // Repositories 
            services.AddScoped<IRepository<ArticleEntity>, ArticleRepository>();
            services.AddScoped<IRepository<ArticleCategoryEntity>, ArticleCategoryRepository>();

            // Mappers
            services.AddAutoMapper();

            // Logging
            services.AddScoped(typeof(ILogger<>), typeof(NLogLogger<>));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
