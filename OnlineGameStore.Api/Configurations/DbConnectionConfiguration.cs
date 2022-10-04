namespace OnlineGameStore.Api.Configurations
{
    public class DbConnectionConfiguration
    {
        public const string SectionName = "DbConnecionStrings";

        public string MsSqlConnectionString { get; set; } = string.Empty;
    }
}
