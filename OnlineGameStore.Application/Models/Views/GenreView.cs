namespace OnlineGameStore.Application.Models.Views
{
    public class GenreView
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid? ParentGenreId { get; set; }
    }
}