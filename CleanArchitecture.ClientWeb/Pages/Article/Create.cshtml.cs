using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CleanArchitecture.ClientWeb.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;

        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        [BindProperty]
        public ArticleDTO Article { get; set; }

        public List<SelectListItem> ArticleCategoryList { get; set; }

        public CreateModel(
            IArticleService articleService,
            IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
        }

        public async Task OnGetAsync()
        {
            await Init();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await Init();

            if (!ModelState.IsValid)
                return Page();

            Article.Category = 
                await _articleCategoryService.GetArticleCategoryAsync(Article.CategoryId);

            if (ImageUpload != null)
            {
                Article.ImageFileName =
                    await _articleService.UploadArticleImageAsync(ImageUpload);
            }

            await _articleService.CreateArticleAsync(Article);

            return RedirectToPage("/Index");
        }

        private async Task Init()
        {
            ArticleCategoryList =
                await BuildCategoryList();
        }

        private async Task<List<SelectListItem>> BuildCategoryList()
        {
            var categories = await _articleCategoryService.ListAllArticleCategoriesAsync();

            var list = categories
                .Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                })
                .ToList();

            return list;
        }
    }
}