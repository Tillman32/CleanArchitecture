using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;


namespace CleanArchitecture.Infrastructure.Database
{
    public class ArticleRepository : IRepository<ArticleEntity>
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<ArticleEntity> AddAsync(ArticleEntity entity)
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

        public async Task DeleteAsync(ArticleEntity entity)
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

        public async Task<ArticleEntity> GetAsync(int id)
        {
            var article = await _db.Articles
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();

            return article;
        }

        public async Task<IEnumerable<ArticleEntity>> ListAllAsync()
        {
            var allArticles =
                await _db.Articles
                .ToListAsync();

            return allArticles;
        }

        public async Task<ArticleEntity> UpdateAsync(ArticleEntity entity)
        {
            _db.Articles.Update(entity);
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
