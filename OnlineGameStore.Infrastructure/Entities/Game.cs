namespace OnlineGameStore.Infrastructure.Entities
{
    public class Game
    {
        public Guid Key { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        public virtual ICollection<Genre>? Genres { get; set; }

        public virtual ICollection<PlatformType>? Platforms { get; set; }

    }
}
