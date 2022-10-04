namespace OnlineGameStore.Application.Models.Views
{
    public class CommentView
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime? DatePosted { get; set; }

        public string? Body { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Guid? GameId { get; set; }
    }
}