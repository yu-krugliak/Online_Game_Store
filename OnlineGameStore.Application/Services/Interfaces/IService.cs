namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<TEntity> GetExistingEntityById(Guid id);
    }
}