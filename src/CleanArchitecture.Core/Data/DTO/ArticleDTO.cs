using System;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Core.Data.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(125)]
        public string Title { get; set; }
        public string ContentPreview { get; set; }

        [Required, MaxLength(1000)]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string ImageFileName { get; set; }

        public int CategoryId { get; set; }
        public ArticleCategoryDTO Category { get; set; }
    }
}
