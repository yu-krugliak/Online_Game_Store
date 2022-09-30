namespace OnlineGameStore.Infrastructure.Entities
{
    public class PlatformType : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string? Type { get; set; }

        public virtual ICollection<Game>? Games { get; set; }

    }
}
