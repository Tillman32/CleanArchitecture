using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.DTO;
using CleanArchitecture.Core.Entity;

namespace CleanArchitecture.Core.Service
{
    public class ArticleCategoryService : IArticleCategoryService
    {

        private readonly IRepository<ArticleCategoryEntity> _repo;
        private readonly IMapper _mapper;

        public ArticleCategoryService(
            IRepository<ArticleCategoryEntity> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleCategoryDTO>> ListAllArticleCategoriesAsync()
        {
            var entities =
                await _repo.ListAllAsync();

            return entities
                .Select(e => _mapper.Map<ArticleCategoryEntity, ArticleCategoryDTO>(e));
        }

        public async Task CreateArticleCategoryAsync(ArticleCategoryDTO category)
        {
            var entity =
                _mapper.Map<ArticleCategoryDTO, ArticleCategoryEntity>(category);

            entity.Title = category.Title;

            await _repo.AddAsync(entity);
        }

        public async Task<ArticleCategoryDTO> GetArticleCategoryAsync(int id)
        {
            var category =
                await _repo.GetAsync(id);

            return _mapper.Map<ArticleCategoryEntity, ArticleCategoryDTO>(category);
        }

    }
}
