using System;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Core.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentPreview { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string ImageFileName { get; set; }

        public int CategoryId { get; set; }
        public ArticleCategoryDTO Category { get; set; }
    }
}
