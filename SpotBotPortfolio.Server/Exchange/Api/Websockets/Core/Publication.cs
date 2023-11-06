namespace SpotBot.Server.Exchange.Api.Websockets.Core
{
    internal class Publication<T>
    {
        public string Type { get; set; }
        public string Topic { get; set; }
        public string Subject { get; set; }
        public T Data { get; set; }
    }
}
