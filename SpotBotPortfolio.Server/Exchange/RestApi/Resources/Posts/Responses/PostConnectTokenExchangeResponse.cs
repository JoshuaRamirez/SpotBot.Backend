using SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses
{
    internal class PostConnectTokenExchangeResponse
    {
        public PostConnectTokenExchangeResponse()
        {
            Code = "";
            Data = new ConnectTokenExchangeShape();
        }
        public string Code { get; set; }
        public ConnectTokenExchangeShape Data { get; set; }
    }
}
