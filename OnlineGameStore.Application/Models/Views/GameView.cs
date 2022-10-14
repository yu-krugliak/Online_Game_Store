namespace OnlineGameStore.Application.Models.Views
{
    public class GameView
    {
        public int Id { get; set; }

        public string? Key { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public List<int>? GenreIds { get; set; }

        public List<int>? PlatformIds { get; set; }
    }
}