using SpotBot.Server.Exchange.Websockets.Core;
using SpotBot.Server.Exchange.Websockets.Publications.Public;

namespace SpotBot.Server.Exchange.Websockets.Subscribers
{
    internal class AllSymbolsTickerSubscriber : Subscriber<AllSymbolsTickerPublication>
    {
        protected override Subscription<AllSymbolsTickerPublication> CreateSubscription()
        {
            return SubscriptionFactory.AllSymbolsTicker(PublicationReceived);
        }
    }
}
