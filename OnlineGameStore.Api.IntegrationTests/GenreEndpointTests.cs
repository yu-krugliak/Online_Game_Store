using AutoFixture;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace OnlineGameStore.Api.IntegrationTests
{
    public class GenreEndpointTests : IClassFixture<ApplicationFactory<Program>>
    {
        private readonly ApplicationFactory<Program> _factory;
        private readonly Fixture _fixture;

        public GenreEndpointTests(ApplicationFactory<Program> factory)
        {
            _factory = factory;
            _fixture = new();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}