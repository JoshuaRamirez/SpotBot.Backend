namespace SpotBot.Server.Exchange.Websockets.Models.Responses.Shapes
{
    public class DataMessage<T>
    {
        public string Type { get; set; }
        public string Topic {get;set;}
        public string Subject {get;set;}
        public T Data {get;set;}
    }
}
