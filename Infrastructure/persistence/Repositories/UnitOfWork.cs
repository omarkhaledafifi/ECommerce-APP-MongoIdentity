using Domain.Contracts;
using Domain.Entities;
using persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly ConcurrentDictionary<string, Object> _repositories;
        //private readonly Dictionary<string, Object> _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            => (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, TKey>(_storeContext));
        //{
        //    var name = typeof(TEntity).Name;
        //    if (_repositories.ContainsKey(name))
        //        return (IGenericRepository<TEntity, TKey>) _repositories[name];
        //    var repo = new GenericRepository<TEntity, TKey>(_storeContext);
        //    _repositories.Add(name, repo);
        //    return new GenericRepository<TEntity, TKey>(_storeContext);
        //}

        public async Task<int> SaveChangesAsync() => await _storeContext.SaveChangesAsync();
    }
}
