using SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses
{
    public class ConnectToken
    {
        public string Code { get; set; }
        public ConnectTokenData Data { get; set; }
    }
}
