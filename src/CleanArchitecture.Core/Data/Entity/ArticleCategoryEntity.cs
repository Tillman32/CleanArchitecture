using System;

namespace CleanArchitecture.Core.Data.Entity
{
    public class ArticleCategoryEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
    }
}
