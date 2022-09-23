namespace OnlineGameStore.Infrastructure.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Guid? Description { get; set; }

        public Guid? ParentGenreId { get; set; }

        public virtual Genre? ParentGenre { get; set; }

        public virtual ICollection<Genre>? NestedGenres { get; set; }

        public virtual ICollection<Game>? Games { get; set; }

    }
}
