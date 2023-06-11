namespace SpotBot.Server.Configuration
{
    public class SpotBotConfigurationModel
    {
        public int? UserTokenTimeToLiveMinutes { get; set; }
        public string? DatabaseName {get;set;}
        public string? DatabaseUserId {get;set;}
        public string? DatabasePassword {get;set;}
        public string DatabaseConnectionString
        {
            get
            {
                return $"Server=localhost;Database={DatabaseName};Uid={DatabaseUserId};Pwd={DatabasePassword};";
            } 
        }
        public string DatabaseServerConnectionString
        {
            get 
            {
                return $"Server=localhost;Uid={DatabaseUserId};Pwd={DatabasePassword};";
            }
        }
    }
}