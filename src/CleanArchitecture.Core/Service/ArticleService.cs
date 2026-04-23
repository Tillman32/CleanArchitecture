using AutoMapper;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Data.Entity;
using CleanArchitecture.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IGenericRepository<ArticleEntity> _articles;
        private readonly IGenericRepository<ArticleCategoryEntity> _categories;
        private readonly IMapper _mapper;

        public ArticleService(
            IGenericRepository<ArticleEntity> articles,
            IGenericRepository<ArticleCategoryEntity> categories,
            IMapper mapper)
        {
            _articles = articles;
            _categories = categories;
            _mapper = mapper;
        }

        private async Task<List<ArticleCategoryEntity>> GetCategoriesAsync() =>
            (await _categories.GetAll()).ToList();

        public async Task<IEnumerable<ArticleDTO>> ListAllArticlesAsync()
        {
            var cats = await GetCategoriesAsync();
            var articles = (await _articles.GetAll())
                .Where(a => !a.IsDraft)
                .OrderByDescending(a => a.DateCreated)
                .ToList();
            foreach (var a in articles)
                a.Category = cats.FirstOrDefault(c => c.Id == a.CategoryId);
            return articles.Select(e => _mapper.Map<ArticleEntity, ArticleDTO>(e));
        }

        public async Task<IEnumerable<ArticleDTO>> ListDraftsAsync()
        {
            var articles = (await _articles.GetAll())
                .Where(a => a.IsDraft)
                .OrderByDescending(a => a.DateCreated)
                .ToList();
            return articles.Select(e => _mapper.Map<ArticleEntity, ArticleDTO>(e));
        }

        public async Task<ArticleDTO?> GetArticleAsync(int id)
        {
            var article = await _articles.GetById(id);
            if (article == null) return null;
            var cats = await GetCategoriesAsync();
            article.Category = cats.FirstOrDefault(c => c.Id == article.CategoryId);
            return _mapper.Map<ArticleEntity, ArticleDTO>(article);
        }

        public async Task CreateArticleAsync(ArticleDTO articleDTO)
        {
            var entity = _mapper.Map<ArticleDTO, ArticleEntity>(articleDTO);
            entity.ContentPreview = entity.Content?.Length > 200
                ? entity.Content!.Truncate(200) + "..."
                : entity.Content ?? "";
            entity.DateCreated = DateTime.UtcNow;
            entity.CreatedBy = "Bob Ross";
            entity.Category = null;
            await _articles.Create(entity);
            articleDTO.Id = entity.Id;
        }

        public async Task UpdateArticleAsync(ArticleDTO articleDTO)
        {
            var entity = _mapper.Map<ArticleDTO, ArticleEntity>(articleDTO);
            entity.ContentPreview = entity.Content?.Length > 200
                ? entity.Content!.Truncate(200) + "..."
                : entity.Content ?? "";
            if (!entity.IsDraft)
                entity.DateCreated = DateTime.UtcNow;
            entity.Category = null;
            await _articles.Update(entity.Id, entity);
        }

        public async Task DeleteArticleAsync(int id) =>
            await _articles.Delete(id);
    }
}
