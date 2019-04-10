using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CleanArchitecture.ClientWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IArticleService _articleService;

        public IEnumerable<ArticleDTO> Articles { get; set; }

        public IndexModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task OnGetAsync()
        {
            Articles =
                await _articleService.ListAllArticlesAsync();
        }
    }
}