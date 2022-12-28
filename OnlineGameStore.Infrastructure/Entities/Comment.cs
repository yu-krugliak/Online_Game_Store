using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Infrastructure.Entities
{
    public class Comment : IEntity<int>
    {
        public int Id { get; set; }

        public DateTime? DatePosted { get; set; }

        public string? Body { get; set; }


        public Guid? OwnerId { get; set; }
        public User? Owner { get; set; }

        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }

        public int? GameId { get; set; }
        public Game? Game { get; set; }

        public ICollection<Comment>? CommentReplies { get; set; }
    }
}
