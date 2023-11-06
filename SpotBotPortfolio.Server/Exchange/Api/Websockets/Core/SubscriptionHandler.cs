using System.Text.Json;

namespace SpotBot.Server.Exchange.Api.Websockets.Core
{
    internal class SubscriptionHandler<T> : TopicHandler
    {
        public SubscriptionHandler(Action<Publication<T>> action)
        {
            _action = action;
            Topic = "";
        }

        private readonly Action<Publication<T>> _action;

        public override string Topic { get; set; }

        public override void Handle(string message)
        {
            var dataMessage = Deserialize(message);
            _action(dataMessage);
        }

        private Publication<T> Deserialize(string message)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var deserializedMessage = JsonSerializer.Deserialize<Publication<T>>(message, options);
            if (deserializedMessage == null)
            {
                throw new ArgumentException(nameof(message), "The deserialized message cannot be null.");
            }
            return deserializedMessage;
        }


    }
}