namespace OnlineGameStore.Infrastructure.Entities
{
    public class Game : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid Key { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }


        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public virtual ICollection<PlatformType> Platforms { get; set; } = new List<PlatformType>();
    }
}
