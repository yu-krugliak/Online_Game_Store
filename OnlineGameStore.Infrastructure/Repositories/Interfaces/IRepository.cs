namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(Guid id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<bool> DeleteByIdAsync(Guid id);

        Task<bool> UpdateAsync(TEntity entity);
    }
}
