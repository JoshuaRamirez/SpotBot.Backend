using SpotBot.Server.Exchange.Api.Websockets.Core;
using SpotBot.Server.Exchange.Api.Websockets.Publications.Public;

namespace SpotBot.Server.Exchange.Api.Websockets.Subscribers
{
    internal class AllSymbolsTickerSubscriber : Subscriber<AllSymbolsTickerPublication>
    {
        protected override Subscription<AllSymbolsTickerPublication> CreateSubscription()
        {
            return SubscriptionFactory.AllSymbolsTicker(PublicationReceived);
        }
    }
}
