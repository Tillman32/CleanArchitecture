using CleanArchitecture.Core.DTO;
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
        public string Title { get; set; }
        [BindProperty]
        public string ArticleContent { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
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
            ArticleCategoryList =
                await BuildCategoryList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var article =
                new ArticleDTO();

            article.Title = Title;
            article.Content = ArticleContent;
            article.CategoryId = CategoryId;
            article.Category = 
                await _articleCategoryService.GetArticleCategoryAsync(CategoryId);

            if(ImageUpload != null)
            {
                article.ImageFileName = 
                    await _articleService.UploadArticleImageAsync(ImageUpload);
            }

            await _articleService.CreateArticleAsync(article);

            return RedirectToPage("/Index");
        }

        private async Task<List<SelectListItem>> BuildCategoryList()
        {
            var list = new List<SelectListItem>();
            var categories = await _articleCategoryService.ListAllArticleCategoriesAsync();

            foreach (var category in categories)
            {
                list.Add(new SelectListItem()
                {
                    Text = category.Title,
                    Value = category.Id.ToString()
                });
            }

            return list;
        }
    }
}