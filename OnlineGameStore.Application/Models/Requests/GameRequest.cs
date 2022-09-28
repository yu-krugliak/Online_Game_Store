namespace OnlineGameStore.Application.Models.Requests
{
    public class GameRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Guid>? GenreId { get; set; }

        public ICollection<Guid>? PlatformId { get; set; }
    }
}