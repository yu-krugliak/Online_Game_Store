namespace OnlineGameStore.Application.Models.Views
{
    public class CommentView
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? DatePosted { get; set; }

        public string? Body { get; set; }

        public int? ParentCommentId { get; set; }

        public int? GameId { get; set; }
    }
}