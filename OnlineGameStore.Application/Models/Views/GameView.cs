namespace OnlineGameStore.Application.Models.Views
{
    public class GameView
    {
        public Guid Id { get; set; }

        public Guid Key { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public ICollection<Guid>? GenreIds { get; set; }

        public ICollection<Guid>? PlatformIds { get; set; }
    }
}