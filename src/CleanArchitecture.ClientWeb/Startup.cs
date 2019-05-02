using AutoMapper;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using CleanArchitecture.Core.Logging;
using CleanArchitecture.Core.Service;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.Database.Repository;
using CleanArchitecture.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "CleanDb"));

            // Repositories
            services.AddScoped<IGenericRepository<ArticleEntity>, GenericRepository<ArticleEntity>>();
            services.AddScoped<IGenericRepository<ArticleCategoryEntity>, GenericRepository<ArticleCategoryEntity>>();

            // Services
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();

            // Mappers
            services.AddAutoMapper();

            // Logging
            services.AddScoped(typeof(ILogger<>), typeof(NLogLogger<>));

            // Framework
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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