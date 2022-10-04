namespace OnlineGameStore.Application.Models.Requests
{
    public class CommentRequest
    {
        public string? Name { get; set; }

        public string? Body { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Guid? GameId { get; set; }
    }
}