using MapsterMapper;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Application.Services.UnitOfWorkImplementation;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommentView> AddAsync(CommentRequest commentRequest)
        {
            var comment = _mapper.Map<Comment>(commentRequest);

            var addedComment = await _unitOfWork.CommentRepository.AddAsync(comment);

            return _mapper.Map<CommentView>(addedComment);
        }

        public async Task<IEnumerable<CommentView>> GetByGame(Guid gameKey)
        {
            var comments = await _unitOfWork.CommentRepository.GetByGameKey(gameKey);

            var commentsViews = _mapper.Map<IEnumerable<CommentView>>(comments);
            return commentsViews;
        }
    }
}
