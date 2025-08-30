using Domain.Contracts;
using Domain.Entities;
using persistence.Data;
using Services.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task AddAsync(TEntity entity) => await _storeContext.Set<TEntity>().AddAsync(entity);

        public async Task<int> CountAsync(ISpecification<TEntity> specifications)
            => await SpecificationsEvaluator.CreateQuery(_storeContext.Set<TEntity>(), specifications).CountAsync();

        public void DeleteAsync(TEntity entity) => _storeContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
            => trackChanges ? await _storeContext.Set<TEntity>().ToListAsync()
            : await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specifications)
            => await SpecificationsEvaluator.CreateQuery(_storeContext.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity?> GetAsync(Tkey id) => await _storeContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetAsync(ISpecification<TEntity> specifications)
            => await SpecificationsEvaluator.CreateQuery(_storeContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public void UpdateAsync(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);
    }
}
