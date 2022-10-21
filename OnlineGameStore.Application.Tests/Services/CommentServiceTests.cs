using AutoFixture;
using FluentAssertions;
using NSubstitute;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Services
{
    public sealed class CommentServiceTests : ServiceTestsBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly CommentService _sut;

        public CommentServiceTests()
        {
            _commentRepository = Substitute.For<ICommentRepository>();
            _sut = new(_commentRepository, _mapper);
        }

        [Fact]
        public async Task GetByGameAsync_WhenNoErrors_ShouldReturnCommentsViews()
        {
            // Arrange
            var gameId = _fixture.Create<int>();

            var comments = _fixture.CreateMany<Comment>(3).ToList();
            var commentsViews = _mapper.Map<IEnumerable<CommentView>>(comments).ToList();
            _commentRepository.GetByGameId(Arg.Any<int>()).Returns(comments);

            // Act
            var result = (await _sut.GetByGameAsync(gameId)).ToList();

            // Assert
            await _commentRepository.Received(1).GetByGameId(gameId);
            result.Should().BeEquivalentTo(commentsViews);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCommentExists_ShouldReturnCommentView()
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
            result.Should().BeEquivalentTo(commentView);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCommentNotExists_ShouldThrowNotFound()
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
