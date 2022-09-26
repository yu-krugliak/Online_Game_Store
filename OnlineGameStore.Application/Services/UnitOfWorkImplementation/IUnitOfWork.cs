using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Repositories.Implementations;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.UnitOfWorkImplementation
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }

        ICommentRepository CommentRepository { get; }

        IGenreRepository GenreRepository { get; }

        IPlatformRepository PlatformRepository { get; }
    }
}
