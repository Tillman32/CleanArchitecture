using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Database.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public GenericRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            using var ctx = _factory.CreateDbContext();
            return await ctx.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetPaginated(int page, int size)
        {
            using var ctx = _factory.CreateDbContext();
            return await ctx.Set<TEntity>()
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            using var ctx = _factory.CreateDbContext();
            return await ctx.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            using var ctx = _factory.CreateDbContext();
            await ctx.Set<TEntity>().AddAsync(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task Update(int id, TEntity entity)
        {
            using var ctx = _factory.CreateDbContext();
            ctx.Set<TEntity>().Update(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            using var ctx = _factory.CreateDbContext();
            var entity = await ctx.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                ctx.Set<TEntity>().Remove(entity);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
