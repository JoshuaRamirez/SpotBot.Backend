namespace SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses.Shapes
{
    public class ConnectTokenShape
    {
        public string Token { get; set; }
        public InstanceServerShape[] InstanceServers { get; set; }
    }
}
