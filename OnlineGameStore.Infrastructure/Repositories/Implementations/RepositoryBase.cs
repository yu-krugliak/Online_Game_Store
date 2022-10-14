using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Infrastructure.Repositories.Implementations
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        private readonly DbContext _gameContext;

        protected RepositoryBase(DbContext context)
        {
            _gameContext = context;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _gameContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _gameContext.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _gameContext.Set<TEntity>().Add(entity);
            await _gameContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var record = await _gameContext.Set<TEntity>().FindAsync(id);

            if (record is null)
            {
                return false;
            }

            _gameContext.Set<TEntity>().Remove(record);
            await _gameContext.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _gameContext.Set<TEntity>().Update(entity);
            await _gameContext.SaveChangesAsync();

            return true;
        }
    }
}
