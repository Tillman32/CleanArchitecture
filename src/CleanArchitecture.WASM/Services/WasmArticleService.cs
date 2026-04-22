using AutoMapper;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Data.Entity;
using CleanArchitecture.Core.Extensions;
using CleanArchitecture.Core.Service;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WASM.Services
{
    public class WasmArticleService : IArticleService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        private readonly IGenericRepository<ArticleEntity> _repo;
        private readonly IMapper _mapper;

        public WasmArticleService(
            IDbContextFactory<ApplicationDbContext> factory,
            IGenericRepository<ArticleEntity> repo,
            IMapper mapper)
        {
            _factory = factory;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleDTO>> ListAllArticlesAsync()
        {
            using var ctx = _factory.CreateDbContext();
            var articles = await ctx.Articles
                .Include(a => a.Category)
                .AsNoTracking()
                .OrderByDescending(a => a.DateCreated)
                .ToListAsync();
            return articles.Select(e => _mapper.Map<ArticleEntity, ArticleDTO>(e));
        }

        public async Task<ArticleDTO?> GetArticleAsync(int id)
        {
            using var ctx = _factory.CreateDbContext();
            var article = await ctx.Articles
                .Include(a => a.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            return article == null ? null : _mapper.Map<ArticleEntity, ArticleDTO>(article);
        }

        public async Task CreateArticleAsync(ArticleDTO articleDTO)
        {
            var entity = _mapper.Map<ArticleDTO, ArticleEntity>(articleDTO);
            entity.ContentPreview = entity.Content?.Length > 200
                ? entity.Content.Truncate(200) + "..."
                : entity.Content ?? "";
            entity.DateCreated = DateTime.UtcNow;
            entity.CreatedBy = "Bob Ross";
            await _repo.Create(entity);
        }

    }
}
