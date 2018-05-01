using CleanArchitecture.Core.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Data
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
