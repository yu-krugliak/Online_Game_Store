namespace OnlineGameStore.Application.Models.Requests
{
    public class CommentRequest
    {
        public string? Body { get; set; }

        public int? ParentCommentId { get; set; }

        public int? GameId { get; set; }
    }
}