using AutoFixture;
using MapsterMapper;
using NSubstitute;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

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
    }
}
