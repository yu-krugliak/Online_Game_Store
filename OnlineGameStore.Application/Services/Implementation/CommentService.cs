using MapsterMapper;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class CommentService : ServiceBase<Comment>, ICommentService
    {
        private ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper) : base(commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;   
        }

        public async Task<IEnumerable<CommentView>> GetByGameAsync(int gameId)
        {
            var comments = await _commentRepository.GetByGameId(gameId);

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
            var comment = _mapper.Map<Comment>(commentRequest);

            comment.DatePosted = DateTime.UtcNow;

            var addedComment = await _commentRepository.AddAsync(comment);

            return _mapper.Map<CommentView>(addedComment);
        }
    }
}
