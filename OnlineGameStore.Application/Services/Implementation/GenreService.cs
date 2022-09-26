using MapsterMapper;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Application.Services.UnitOfWorkImplementation;
using OnlineGameStore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenreView> AddAsync(GenreRequest genreRequest)
        {
            var genre = _mapper.Map<Game>(genreRequest);

            var addedGenre = await _unitOfWork.GameRepository.AddAsync(genre);

            return _mapper.Map<GenreView>(addedGenre);
        }

    }
}
