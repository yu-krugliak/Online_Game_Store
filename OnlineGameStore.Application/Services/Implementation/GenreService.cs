using MapsterMapper;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Extensions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class GenreService : ServiceBase<Genre>,  IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper) : base(genreRepository)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreView>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();

            var genreViews = _mapper.Map<IEnumerable<GenreView>>(genres);
            return genreViews;
        }

        public async Task<GenreView> GetByIdAsync(int genreId)
        {
            var genre = await GetExistingEntityById(genreId);

            var genreView = _mapper.Map<GenreView>(genre);
            return genreView;
        }

        public async Task<GenreView> AddAsync(GenreRequest genreRequest)
        {
            if (genreRequest.ParentGenreId.TryGetValue(out var parentGenreId))
            {
                await ThrowIfGenreNotExists(parentGenreId);
            }

            var genre = _mapper.Map<Genre>(genreRequest);
            var addedGenre = await _genreRepository.AddAsync(genre);

            return _mapper.Map<GenreView>(addedGenre);
        }

        private async Task ThrowIfGenreNotExists(int parentId)
        {
            var isParentExists = await _genreRepository.ExistsAsync(parentId);
            if (!isParentExists)
            {
                throw new NotFoundException($"Parent {typeof(Genre).Name} with such id doesn't exist.");
            }
        }
    }
}
