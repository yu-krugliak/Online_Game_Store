namespace OnlineGameStore.Infrastructure.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Body { get; set; }

        public DateTime? DatePosted { get; set; }

        public Guid? ParentCommentId { get; set; }

        public virtual Comment? ParentComment { get; set; }

        public virtual ICollection<Comment>? CommentReplies { get; set; }

        public Guid? GameId { get; set; }

        public virtual Game? Game { get; set; }
    }
}
