namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<bool> UpdateAsync(TEntity entity);
    }
}
