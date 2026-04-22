using AutoMapper;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using CleanArchitecture.Core.Mapping;
using CleanArchitecture.Core.Service;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.Database.Repository;
using CleanArchitecture.WASM.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<CleanArchitecture.WASM.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// EF Core + SQLite (in-memory for now; swap connection string for OPFS persistence)
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=bobross.db"));

// Repositories
builder.Services.AddScoped<IGenericRepository<ArticleEntity>, GenericRepository<ArticleEntity>>();
builder.Services.AddScoped<IGenericRepository<ArticleCategoryEntity>, GenericRepository<ArticleCategoryEntity>>();

// Services
builder.Services.AddScoped<IArticleService, WasmArticleService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();

// AutoMapper (manual registration — no DI extension package needed in WASM)
builder.Services.AddSingleton<IMapper>(_ =>
    new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper());

var host = builder.Build();

// Seed the local database on first run
var dbFactory = host.Services.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
using (var db = dbFactory.CreateDbContext())
{
    await ApplicationDbInitializer.Initialize(db);
}

await host.RunAsync();
