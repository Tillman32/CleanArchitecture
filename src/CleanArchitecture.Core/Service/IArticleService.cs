using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Data.DTO;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Core.Service
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDTO>> ListAllArticlesAsync();
        Task<ArticleDTO> GetArticleAsync(int id);
        Task CreateArticleAsync(ArticleDTO article);
        Task<string> UploadArticleImageAsync(IFormFile imageFile);
    }
}
