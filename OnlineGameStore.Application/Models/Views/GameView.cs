namespace OnlineGameStore.Application.Models.Views
{
    public class GameView
    {
        public Guid Id { get; set; }

        public string? Key { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public List<Guid>? GenreIds { get; set; }

        public List<Guid>? PlatformIds { get; set; }
    }
}