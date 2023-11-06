using SpotBot.Server.Exchange.Api.Rest.Responses.Shapes;

namespace SpotBot.Server.Exchange.Api.Rest.Responses
{
    internal class PostExchangeConnectTokenRestResponse
    {
        public PostExchangeConnectTokenRestResponse()
        {
            Code = "";
            Data = new ConnectTokenExchangeShape();
        }
        public string Code { get; set; }
        public ConnectTokenExchangeShape Data { get; set; }
    }
}
