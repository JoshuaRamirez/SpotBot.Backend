namespace SpotBot.Server.Exchange.Websockets.Core
{

    public class Subscription<T>
    {
        public Subscription(Action<Publication<T>> receiveAction)
        {
            Id = Guid.NewGuid();
            Type = string.Empty;
            Topic = string.Empty;
            PrivateChannel = false;
            Response = false;
            ReceiveAction = receiveAction;
        }
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Topic { get; set; }
        public bool PrivateChannel { get; set; }
        public bool Response { get; set; }
        public Action<Publication<T>> ReceiveAction { get; set; }
    }
}
