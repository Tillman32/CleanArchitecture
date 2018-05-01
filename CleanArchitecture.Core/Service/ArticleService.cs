using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.DTO;
using CleanArchitecture.Core.Entity;
using CleanArchitecture.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AutoMapper;

namespace CleanArchitecture.Core.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<ArticleEntity> _repo;
        private readonly IMapper _mapper;

        public ArticleService(
            IHostingEnvironment hostingEnvironment,
            IRepository<ArticleEntity> repo,
            IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleDTO>> ListAllArticlesAsync()
        {
            var entities = 
                await _repo.ListAllAsync();

            return entities
                .Select(e => _mapper.Map<ArticleEntity, ArticleDTO>(e));
        }

        public async Task<ArticleDTO> GetArticleAsync(int id)
        {
            var article =
                await _repo.GetAsync(id);

            return _mapper.Map<ArticleEntity, ArticleDTO>(article);
        }

        public async Task CreateArticleAsync(ArticleDTO article)
        {
            var entity =
                _mapper.Map<ArticleDTO, ArticleEntity>(article);

            entity.ContentPreview = $"{entity.Content.Truncate(500)}...";
            entity.DateCreated = DateTime.Now.Date;
            entity.CreatedBy = "BrandonTillman.com";

            await _repo.AddAsync(entity);
        }

        public async Task<string> UploadArticleImageAsync(IFormFile imageFile)
        {
            var fileName = GetUniqueFileName(imageFile.FileName);
            var uploadsDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsDir, fileName);

            try
            {
                await imageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
            }
            catch (Exception)
            {
                // TODO: Log Exception
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
