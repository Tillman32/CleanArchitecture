using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CleanArchitecture.ClientWeb.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        [BindProperty]
        public ArticleDTO Article { get; set; }

        public List<SelectListItem> ArticleCategoryList { get; set; }

        public CreateModel(
            IArticleService articleService,
            IArticleCategoryService articleCategoryService,
            IWebHostEnvironment env)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
            _env = env;
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
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsDir);
                var ext = Path.GetExtension(ImageUpload.FileName);
                var fileName = $"{Guid.NewGuid()}{ext}";
                using var stream = new FileStream(Path.Combine(uploadsDir, fileName), FileMode.Create);
                await ImageUpload.CopyToAsync(stream);
                Article.ImageFileName = fileName;
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