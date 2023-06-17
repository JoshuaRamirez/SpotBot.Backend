public abstract class TopicHandler
{
    public abstract string Topic { get; set; }
    public abstract void Handle(string message);
}
