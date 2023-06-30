using SpotBot.Server.Exchange.Websockets.Core;
using SpotBot.Server.Exchange.Websockets.Requests;
using SpotBot.Server.Exchange.Websockets.Responses.Public;

namespace SpotBot.Server.Exchange.Websockets.Subscribers
{
    public class AllSymbolsTickerSubscriber : Subscriber<AllSymbolsTickerPublication>
    {
        protected override Subscription<AllSymbolsTickerPublication> CreateSubscription()
        {
            return SubscriptionFactory.AllSymbolsTicker(PublicationReceived);
        }
    }
}
