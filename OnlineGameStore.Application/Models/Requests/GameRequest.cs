namespace OnlineGameStore.Application.Models.Requests
{
    public class GameRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }


        public List<Guid> GenreIds { get; set; } = new();

        public List<Guid> PlatformIds { get; set; } = new();
    }
}