using FluentAssertions;
using MapsterMapper;
using Moq;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Service
{
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly ICommentService _sut;
        private readonly Mock<IMapper> _mapperMock;

        public CommentServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _commentRepository = new Mock<ICommentRepository>();
            _sut = new CommentService(_commentRepository.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetCommentsByGameId_WhenListIsEmpty_ThenEmptyListReturned()
        {
            var game = new Game() { Id = Guid.NewGuid() };
            var result = await _sut.GetByGame(game.Id);
            result.Should().BeNullOrEmpty();

            _commentRepository.Verify(x => x.GetByGameId(game.Id), Times.Once);
            _commentRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetCommentsByGameId_WhenListIsNotEmpty_ThenListReturned()
        {
            var game = new Game() { Id = Guid.NewGuid() };

            IEnumerable<Comment> listComments = Enumerable.Empty<Comment>();
            listComments.Append(
                new Comment()
                {
                    Id = Guid.NewGuid(),
                    Name = "David",
                    GameId = game.Id
                });
            listComments.Append(
                new Comment()
                {
                    Id = Guid.NewGuid(),
                    Name = "Scott",
                    GameId = game.Id
                }
            );

            IEnumerable<CommentView> listCommentsViews = Enumerable.Empty<CommentView>();
            listCommentsViews.Append(
                new CommentView()
                {
                    Id = Guid.NewGuid(),
                    Name = "David",
                    GameId = game.Id
                });
            listCommentsViews.Append(
                new CommentView()
                {
                    Id = Guid.NewGuid(),
                    Name = "Scott",
                    GameId = game.Id
                }
            );

            _commentRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(listComments));
            var result = await _sut.GetByGame(game.Id);

            _mapperMock.Setup(maper => maper.Map<IEnumerable<CommentView>>(It.IsAny<IEnumerable<Comment>>()))
               .Returns(listCommentsViews);
            result.Should().Equal(listCommentsViews);

            _commentRepository.Verify(x => x.GetByGameId(game.Id), Times.Once);
            _commentRepository.VerifyNoOtherCalls();
        }
    }
}
