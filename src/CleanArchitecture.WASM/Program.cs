using AutoMapper;
using Blazored.LocalStorage;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using CleanArchitecture.Core.Mapping;
using CleanArchitecture.Core.Service;
using CleanArchitecture.WASM.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<CleanArchitecture.WASM.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Browser localStorage persistence
builder.Services.AddBlazoredLocalStorage();

// Repositories — localStorage-backed, swap for GenericRepository<T> on a server project
builder.Services.AddScoped<IGenericRepository<ArticleEntity>, LocalStorageRepository<ArticleEntity>>();
builder.Services.AddScoped<IGenericRepository<ArticleCategoryEntity>, LocalStorageRepository<ArticleCategoryEntity>>();

// Services
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();

// Seeder (called from App.razor after JS interop is ready)
builder.Services.AddScoped<LocalStorageDbInitializer>();

// AutoMapper
builder.Services.AddSingleton<IMapper>(_ =>
    new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper());

await builder.Build().RunAsync();
