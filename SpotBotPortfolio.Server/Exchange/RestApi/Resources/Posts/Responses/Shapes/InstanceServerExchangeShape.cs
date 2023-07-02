namespace SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses.Shapes
{
    internal class InstanceServerExchangeShape
    {
        public InstanceServerExchangeShape() {
            Endpoint = "";
            Protocol = "";
        }
        public string Endpoint { get; set; }
        public bool Encrypt { get; set; }
        public string Protocol { get; set; }
        public int PingInterval { get; set; }
        public int PingTimeout { get; set; }
    }
}
