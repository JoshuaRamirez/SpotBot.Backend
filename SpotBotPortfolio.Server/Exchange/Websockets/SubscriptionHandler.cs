using SpotBot.Server.Exchange.Websockets.Models.Responses.Shapes;
using System.Text.Json;

namespace SpotBot.Server.Exchange.Websockets
{
    public class SubscriptionHandler<T> : TopicHandler
    {
        public SubscriptionHandler(Action<DataMessage<T>> action)
        {
            _action = action;
            Topic = "";
        }

        private readonly Action<DataMessage<T>> _action;

        public override string Topic { get; set; }

        public override void Handle(string message)
        {
            var dataMessage = Deserialize(message);
            _action(dataMessage);
        }

        private DataMessage<T> Deserialize(string message)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var deserializedMessage = JsonSerializer.Deserialize<DataMessage<T>>(message, options);
            if (deserializedMessage == null)
            {
                throw new ArgumentException(nameof(message), "The deserialized message cannot be null.");
            }
            return deserializedMessage;
        }


    }
}