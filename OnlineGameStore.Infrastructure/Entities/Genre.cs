namespace OnlineGameStore.Infrastructure.Entities
{
    public class Genre : IEntity<int>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }


        public int? ParentGenreId { get; set; }
        public virtual Genre? ParentGenre { get; set; }

        public virtual ICollection<Genre>? NestedGenres { get; set; }

        public virtual ICollection<Game>? Games { get; set; }
    }
}
