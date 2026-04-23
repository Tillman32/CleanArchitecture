using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Data.DTO;

namespace CleanArchitecture.Core.Service
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDTO>> ListAllArticlesAsync();
        Task<IEnumerable<ArticleDTO>> ListDraftsAsync();
        Task<ArticleDTO?> GetArticleAsync(int id);
        Task CreateArticleAsync(ArticleDTO article);
        Task UpdateArticleAsync(ArticleDTO article);
        Task DeleteArticleAsync(int id);
    }
}
