using AutoFixture;
using MapsterMapper;
using OnlineGameStore.Application.Mapster;

namespace OnlineGameStore.Application.Tests.Services;

public class ServiceTestsBase
{
    protected readonly Fixture _fixture;
    protected readonly IMapper _mapper;

    public ServiceTestsBase()
    {
        _fixture = new();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _mapper = new Mapper(MapsterConfiguration.GetConfiguration());
    }
}