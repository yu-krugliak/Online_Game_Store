﻿namespace OnlineGameStore.Infrastructure.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
