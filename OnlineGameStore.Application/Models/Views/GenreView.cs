namespace OnlineGameStore.Application.Models.Views
{
    public class GenreView
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? ParentGenreId { get; set; }
    }
}