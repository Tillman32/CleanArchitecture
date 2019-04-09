using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Database
{
    public class ArticleCategoryRepository : IRepository<ArticleCategoryEntity>
    {
        private readonly ApplicationDbContext _db;

        public ArticleCategoryRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<ArticleCategoryEntity> AddAsync(ArticleCategoryEntity entity)
        {
            try
            {
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                // TODO: Log Exception
                return null;
            }
        }

        public async Task DeleteAsync(ArticleCategoryEntity entity)
        {
            try
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                // TODO: Log Error
            }
        }

        public async Task<ArticleCategoryEntity> GetAsync(int id)
        {
            var category = await _db.ArticleCategories
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();

            return category;
        }

        public async Task<IEnumerable<ArticleCategoryEntity>> ListAllAsync()
        {
            var allCategories =
                await _db.ArticleCategories.ToListAsync();

            return allCategories;
        }

        public async Task<ArticleCategoryEntity> UpdateAsync(ArticleCategoryEntity entity)
        {
            _db.ArticleCategories.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }
    }
}
