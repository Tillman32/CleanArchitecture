using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Core.Data.Entity;

namespace CleanArchitecture.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public virtual DbSet<ArticleEntity> Articles { get; set; }
        public virtual DbSet<ArticleCategoryEntity> ArticleCategories { get; set; }
    }
}
