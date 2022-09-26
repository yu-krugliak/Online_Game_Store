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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IGameRepository _gameRepository;
        private ICommentRepository _commentRepository;
        private IGenreRepository _genreRepository;
        private IPlatformRepository _platformRepository;

        private readonly GamesContext _gamesContext;

        public UnitOfWork(GamesContext gamesContext, IGameRepository gameRepository, ICommentRepository commentRepository,
            IGenreRepository genreRepository, IPlatformRepository platformRepository)
        {
            _gamesContext = gamesContext;
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
        }

        public IGameRepository GameRepository => _gameRepository;

        public ICommentRepository CommentRepository => _commentRepository;

        public IGenreRepository GenreRepository => _genreRepository;

        public IPlatformRepository PlatformRepository => _platformRepository;

        public void Save()
        {
            _gamesContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _gamesContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
