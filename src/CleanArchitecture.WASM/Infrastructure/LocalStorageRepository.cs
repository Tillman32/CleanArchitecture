using Blazored.LocalStorage;
using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WASM.Infrastructure
{
    public class LocalStorageRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ILocalStorageService _storage;
        private readonly string _key = typeof(TEntity).Name;

        public LocalStorageRepository(ILocalStorageService storage)
        {
            _storage = storage;
        }

        private async Task<List<TEntity>> ReadAll() =>
            await _storage.GetItemAsync<List<TEntity>>(_key) ?? new();

        private async Task WriteAll(List<TEntity> items) =>
            await _storage.SetItemAsync(_key, items);

        public async Task<IEnumerable<TEntity>> GetAll() => await ReadAll();

        public async Task<IEnumerable<TEntity>> GetPaginated(int page, int size)
        {
            var all = await ReadAll();
            return all.Skip((page - 1) * size).Take(size);
        }

        public async Task<TEntity> GetById(int id)
        {
            var all = await ReadAll();
            return all.FirstOrDefault(e => e.Id == id)!;
        }

        public async Task Create(TEntity entity)
        {
            var all = await ReadAll();
            if (entity.Id == 0)
                entity.Id = all.Count > 0 ? all.Max(e => e.Id) + 1 : 1;
            all.Add(entity);
            await WriteAll(all);
        }

        public async Task Update(int id, TEntity entity)
        {
            var all = await ReadAll();
            var i = all.FindIndex(e => e.Id == id);
            if (i >= 0)
                all[i] = entity;
            await WriteAll(all);
        }

        public async Task Delete(int id)
        {
            var all = await ReadAll();
            all.RemoveAll(e => e.Id == id);
            await WriteAll(all);
        }
    }
}
