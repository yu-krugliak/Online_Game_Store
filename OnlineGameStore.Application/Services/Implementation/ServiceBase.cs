﻿using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class ServiceBase<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;

        public ServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> GetExistingEntityById(Guid id)
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