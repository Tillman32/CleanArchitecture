using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Data.Entity;
using CleanArchitecture.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AutoMapper;
using CleanArchitecture.Core.Logging;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Core.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGenericRepository<ArticleEntity> _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(
            IHostingEnvironment hostingEnvironment,
            IGenericRepository<ArticleEntity> repo,
            IMapper mapper,
            ILogger<ArticleService> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<IEnumerable<ArticleDTO>> ListAllArticlesAsync()
        {
            try
            {
                var articles =
                    await _repo.GetAll().Take(10).ToListAsync();

                _logger.LogInfo("Retrieved all Article Entities from ArticleService.");

                var articlesDTO =
                    articles.Select(e => _mapper.Map<ArticleEntity, ArticleDTO>(e));

                return articlesDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to ListAllArticles from ArticleService.");
                return null;
            }
        }

        public async Task<ArticleDTO> GetArticleAsync(int id)
        {
            try
            {
                var article =
                    await _repo.GetById(id);

                var articleDTO = 
                    _mapper.Map<ArticleEntity, ArticleDTO>(article);

                _logger.LogInfo($"Retrieved single Article from ArticleService. ID: {article.Id}");

                return articleDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to retrieve Article from ArticleService. ID: {id}");

                return null;
            }
        }

        public async Task CreateArticleAsync(ArticleDTO articleDTO)
        {

            try
            {
                var articleEntity = 
                    _mapper.Map<ArticleDTO, ArticleEntity>(articleDTO);

                articleEntity.ContentPreview = articleEntity.Content.Length > 500 ? $"{articleEntity.Content.Truncate(500)}..." : $"{articleEntity.Content}";
                articleEntity.DateCreated = DateTime.Now.Date;
                articleEntity.CreatedBy = "BrandonTillman.com";

                await _repo.Create(articleEntity);

                _logger.LogInfo($"Created new Article in ArticleService. ID: {articleEntity.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create new Article in ArticleService.");
            }
        }

        public async Task<string> UploadArticleImageAsync(IFormFile imageFile)
        {
            var fileName = GetUniqueFileName(imageFile.FileName);
            var uploadsDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsDir, fileName);

            try
            {
                await imageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

                _logger.LogInfo($"Successfully uploaded new ArticleImage in ArticleService");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to upload new ArticleImage in ArticleService");
            }

            return fileName;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
