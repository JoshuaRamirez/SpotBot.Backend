namespace SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses.Shapes
{
    public class ConnectTokenData
    {
        public string Token { get; set; }
        public ConnectTokenInstanceServer[] InstanceServers { get; set; }
    }
}
