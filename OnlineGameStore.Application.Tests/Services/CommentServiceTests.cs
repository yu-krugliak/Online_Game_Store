using AutoFixture;
using FluentAssertions;
using MapsterMapper;
using NSubstitute;
using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Services
{
    public class CommentServiceTests
    {
        private readonly ICommentRepository _commentRepository;
        private readonly CommentService _sut;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture = new();

        public CommentServiceTests()
        {
            _commentRepository = Substitute.For<ICommentRepository>();
            _mapper = Substitute.For<IMapper>();
            _mapper = new Mapper(MapsterConfiguration.GetConfiguration());
            _sut = new(_commentRepository, _mapper);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetByGameAsync_WhenNoErrors_ShouldReturnCommentsViews()
        {
            // Arrange
            var gameId = _fixture.Create<int>();

            var comments = _fixture.CreateMany<Comment>(3).ToList();
            var commentViews = _mapper.Map<IEnumerable<CommentView>>(comments).ToList();
            _commentRepository.GetByGameId(Arg.Any<int>()).Returns(comments);

            // Act
            var result = (await _sut.GetByGameAsync(gameId)).ToList();

            // Assert
            await _commentRepository.Received(1).GetByGameId(gameId);
            result.Should().Equal(commentViews, (resView, expView) => resView.Id == expView.Id);
            result.Should().Equal(commentViews, (resView, expView) => resView.Name == expView.Name);
            result.Should().Equal(commentViews, (resView, expView) => resView.Body == expView.Body);
            result.Should().Equal(commentViews, (resView, expView) => resView.DatePosted == expView.DatePosted);
            result.Should().Equal(commentViews, (resView, expView) => resView.ParentCommentId == expView.ParentCommentId);
            result.Should().Equal(commentViews, (resView, expView) => resView.GameId == expView.GameId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenExists_ShouldReturnCommentView()
        {
            // Arrange
            var id = _fixture.Create<int>();

            var comment = _fixture.Create<Comment>();
            var commentView = _mapper.Map<CommentView>(comment);
            _commentRepository.GetByIdAsync(Arg.Any<int>()).Returns(comment);

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            await _commentRepository.Received(1).GetByIdAsync(id);

            result.Id.Should().Be(commentView.Id);
            result.Name.Should().Be(commentView.Name);
            result.Body.Should().Be(commentView.Body);
            result.DatePosted.Should().Be(commentView.DatePosted);
            result.ParentCommentId.Should().Be(commentView.ParentCommentId);
            result.GameId.Should().Be(commentView.GameId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotExists_ShouldThrowNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _commentRepository.GetByIdAsync(Arg.Any<int>()).Returns((Comment?)null);

            // Act
            Func<Task> act = async () => await _sut.GetByIdAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Comment with such id doesn't exist.");

            await _commentRepository.Received(1).GetByIdAsync(id);
        }

        [Fact]
        public async Task AddAsync_WhenNoErrors_ShouldPassToRepository()
        {
            // Arrange
            var commentRequest = _fixture.Create<CommentRequest>();
            var comment = _mapper.Map<Comment>(commentRequest);
            var commentView = _mapper.Map<CommentView>(comment);

            _commentRepository.AddAsync(Arg.Any<Comment>()).Returns(comment);

            // Act
            var result = await _sut.AddAsync(commentRequest);

            // Assert
            result.Should().BeEquivalentTo(commentView);
            await _commentRepository.Received(1).AddAsync(Arg.Any<Comment>());
        }
    }
}
