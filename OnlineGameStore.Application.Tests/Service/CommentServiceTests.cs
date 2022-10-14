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
    }
}
