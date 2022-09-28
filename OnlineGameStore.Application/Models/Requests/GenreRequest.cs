namespace OnlineGameStore.Application.Models.Requests
{
    public class GenreRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid? ParentGenreId { get; set; }
    }
}