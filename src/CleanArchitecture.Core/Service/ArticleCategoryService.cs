using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Data.Entity;

namespace CleanArchitecture.Core.Service
{
    public class ArticleCategoryService : IArticleCategoryService
    {

        private readonly IGenericRepository<ArticleCategoryEntity> _repo;
        private readonly IMapper _mapper;

        public ArticleCategoryService(
            IGenericRepository<ArticleCategoryEntity> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleCategoryDTO>> ListAllArticleCategoriesAsync()
        {
            var entities =
                await _repo.GetAll();

            return entities
                .Select(e => _mapper.Map<ArticleCategoryEntity, ArticleCategoryDTO>(e));
        }

        public async Task CreateArticleCategoryAsync(ArticleCategoryDTO category)
        {
            var entity =
                _mapper.Map<ArticleCategoryDTO, ArticleCategoryEntity>(category);

            entity.Title = category.Title;

            await _repo.Create(entity);
        }

        public async Task<ArticleCategoryDTO> GetArticleCategoryAsync(int id)
        {
            var category =
                await _repo.GetById(id);

            return _mapper.Map<ArticleCategoryEntity, ArticleCategoryDTO>(category);
        }

    }
}
