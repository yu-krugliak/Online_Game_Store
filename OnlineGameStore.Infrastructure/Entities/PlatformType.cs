namespace OnlineGameStore.Infrastructure.Entities
{
    public class PlatformType : IEntity<int>
    {
        public int Id { get; set; }
        
        public string? Type { get; set; }


        public virtual ICollection<Game>? Games { get; set; }
    }
}
