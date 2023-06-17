using SpotBot.Server.Exchange.Websockets.Models.Responses.Shapes;

namespace SpotBot.Server.Exchange.Websockets
{

    public class Subscription<T>
    {
        public Subscription(Action<DataMessage<T>> receiveAction)
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
        public Action<DataMessage<T>> ReceiveAction { get; set; }
    }
}
