using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class ServiceBase<TEntity> : IService<TEntity> where TEntity : class, IEntity<int>
    {
        private readonly IRepository<TEntity> _repository;

        public ServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> GetExistingEntityById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"{typeof(TEntity).Name} with such id doesn't exist.");
            }

            return entity;
        }
    }
}
