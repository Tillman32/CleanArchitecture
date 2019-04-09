using CleanArchitecture.Core.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Service
{
    public interface IArticleCategoryService
    {
        Task<IEnumerable<ArticleCategoryDTO>> ListAllArticleCategoriesAsync();
        Task<ArticleCategoryDTO> GetArticleCategoryAsync(int id);
        Task CreateArticleCategoryAsync(ArticleCategoryDTO category);
    }
}
