namespace OnlineGameStore.Application.Models
{
    public class ErrorInfoModel
    {
        public string? Source { get; set; }

        public string? Exception { get; set; }

        public string? StackTrace { get; set; }

        public List<string> Messages { get; set; } = new();
    }
}
