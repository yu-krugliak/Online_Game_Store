namespace OnlineGameStore.Application.Models.Requests
{
    public class GameRequest
    {
        public string? Key { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public List<int> GenreIds { get; set; } = new();

        public List<int> PlatformIds { get; set; } = new();
    }
}