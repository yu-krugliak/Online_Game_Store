using MapsterMapper;
using OnlineGameStore.Application.Auth;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Extensions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class CommentService : ServiceBase<Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, ICurrentUser currentUser) : base(commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;   
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<CommentView>> GetByGameAsync(int gameId)
        {
            var comments = await _commentRepository.GetByGameIdAsync(gameId);

            var commentsViews = _mapper.Map<IEnumerable<CommentView>>(comments);
            return commentsViews;
        }

        public async Task<CommentView> GetByIdAsync(int commentId)
        {
            var comment = await GetExistingEntityById(commentId);

            var commentView = _mapper.Map<CommentView>(comment);
            return commentView;
        }

        public async Task<CommentView> AddAsync(CommentRequest commentRequest)
        {
            if (commentRequest.ParentCommentId.TryGetValue(out var parentCommentId)) 
            {
                await ThrowIfCommentNotExists(parentCommentId);
            }

            var comment = _mapper.Map<Comment>(commentRequest);
            comment.DatePosted = DateTime.UtcNow;
            comment.OwnerId = Guid.Parse(_currentUser.GetUserId());

            var addedComment = await _commentRepository.AddAsync(comment);

            return _mapper.Map<CommentView>(addedComment);
        }

        public async Task UpdateAsync(int commentId, CommentRequest commentRequest)
        {
            if (commentRequest.ParentCommentId.TryGetValue(out var parentCommentId))
            {
                await ThrowIfCommentNotExists(parentCommentId);
            }

            var comment = await GetExistingEntityById(commentId);
            _mapper.Map(commentRequest, comment);

            ThrowForbiddenIfNotOwner(comment.OwnerId);

            var result = await _commentRepository.UpdateAsync(comment);

            if (!result)
            {
                throw new ServerErrorException("Can't update this post.", null);
            }
        }

        public async Task DeleteByIdAsync(int commentId)
        {
            var comment = await GetExistingEntityById(commentId);

            ThrowForbiddenIfNotOwner(comment.OwnerId);

            var result = await _commentRepository.DeleteByIdAsync(commentId);

            if (!result)
            {
                throw new ServerErrorException("Can't delete this post.", null);
            }
        }

        private async Task ThrowIfCommentNotExists(int parentId)
        {
            var isParentExists = await _commentRepository.ExistsAsync(parentId);
            if (!isParentExists)
            {
                throw new NotFoundException($"Parent {typeof(Comment).Name} with such id doesn't exist.");
            }
        }

        private void ThrowForbiddenIfNotOwner(Guid? ownerId)
        {
            if (Guid.Parse(_currentUser.GetUserId()) != ownerId)
            {
                throw new ForbiddenException("Comment doesn't belong to this user");
            }
        }
    }
}