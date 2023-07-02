using SpotBot.Server.Exchange.RestApi.Responses.Shapes;

namespace SpotBot.Server.Domain
{
    internal class SpotGridThing
    {
        private List<KLineExchangeShape> kLines;

        public SpotGridThing(List<KLineExchangeShape> kLines)
        {
            this.kLines = kLines;
        }

        public decimal RunBacktest(decimal initialCapital, decimal gridStepPercentage)
        {
            decimal capital = initialCapital;
            decimal currentPosition = 0m;
            decimal previousClose = 0m;
            var tradeSize = capital / 100;

            for (int i = 0; i < kLines.Count; i++)
            {
                var kLine = kLines[i];
                // Calculate the grid step size as a percentage of the current price
                if (previousClose == 0m)
                {
                    previousClose = kLine.Close;
                }
                var gridStepSize = previousClose * gridStepPercentage;

                // Check if the space between high/low and close triggers a buy/sell
                var buyTarget = previousClose - gridStepSize;
                var sellTarget = previousClose + gridStepSize;
                var triggerBuy = kLine.Low <= buyTarget;
                var triggerSell = kLine.High >= sellTarget;

                if (currentPosition == 0)
                {
                    triggerBuy = true;
                    triggerSell = false;
                    buyTarget = kLine.Close;
                }

                var triggered = triggerBuy || triggerSell;

                if (triggered)
                {
                    // Calculate the number of units to buy/sell based on available capital/position
                    var unitsToTrade = 0m;
                    if (triggerBuy && capital > tradeSize) {
                        unitsToTrade = tradeSize / buyTarget;
                    } else
                    if (triggerBuy && capital < tradeSize) {
                        unitsToTrade = capital / buyTarget;
                    } else
                    if (triggerSell && (currentPosition * sellTarget) > tradeSize) {
                        unitsToTrade = tradeSize / sellTarget;
                    } else
                    if (triggerSell && (currentPosition * sellTarget) < tradeSize) {
                        unitsToTrade = currentPosition  / sellTarget;
                    }

                    var unitsTraded = Math.Floor(unitsToTrade * 100000000) / 100000000; // Round to 8 decimal places

                    if (triggerBuy)
                    {
                        var tradeValue = unitsTraded * buyTarget;
                        currentPosition += unitsTraded;
                        capital -= tradeValue;  // Deduct the spent capital
                        Console.WriteLine($"Time: {kLine.Time}, Buy @ Price: {buyTarget}, Position: {currentPosition}, Capital: {capital}");
                    }
                    else if (triggerSell)
                    {
                        var tradeValue = unitsTraded * sellTarget;
                        currentPosition -= unitsTraded;
                        capital += tradeValue;  // Add the earned capital
                        Console.WriteLine($"Time: {kLine.Time}, Sell @ Price: {sellTarget}, Position: {currentPosition}, Capital: {capital}");
                    }
                }
                previousClose = kLine.Close;
            }

            // Calculate final PnL (Profit and Loss)
            decimal finalPnL = capital + currentPosition * kLines[kLines.Count - 1].Close - initialCapital;
            Console.WriteLine($"Final PnL: {finalPnL}");
            return finalPnL;
        }
    }
}
