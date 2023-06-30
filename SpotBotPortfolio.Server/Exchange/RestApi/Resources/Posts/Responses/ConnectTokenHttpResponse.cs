using SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses
{
    public class ConnectTokenHttpResponse
    {
        public string Code { get; set; }
        public ConnectTokenShape Data { get; set; }
    }
}
