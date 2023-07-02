namespace SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses.Shapes
{
    internal class ConnectTokenExchangeShape
    {
        public ConnectTokenExchangeShape() {
            Token = "";
            InstanceServers = new List<InstanceServerExchangeShape>();
        }
        public string Token { get; set; }
        public List<InstanceServerExchangeShape> InstanceServers { get; set; }
    }
}
