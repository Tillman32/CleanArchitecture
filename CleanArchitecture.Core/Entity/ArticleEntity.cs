using CleanArchitecture.Core.DTO;

namespace CleanArchitecture.Core.Entity
{
    public class ArticleEntity : EntityBase
    {
        public string Title { get; set; }
        public string ContentPreview { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public string ImageFileName { get; set; }

        public virtual ArticleCategoryDTO Category { get; set; }
    }
}
