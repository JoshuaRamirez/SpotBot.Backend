namespace SpotBot.Server.Exchange.Api.Rest.Responses.Shapes
{
    internal class ConnectTokenExchangeShape
    {
        public ConnectTokenExchangeShape()
        {
            Token = "";
            InstanceServers = new List<InstanceServerExchangeShape>();
        }
        public string Token { get; set; }
        public List<InstanceServerExchangeShape> InstanceServers { get; set; }
    }
}
