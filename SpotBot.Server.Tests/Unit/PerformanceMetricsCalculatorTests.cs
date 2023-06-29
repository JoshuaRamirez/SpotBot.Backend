using SpotBot.Server.Domain;

namespace Spotbot.Server.Tests.Unit
{

    [TestFixture]
    public class PerformanceMetricsCalculatorTests
    {
        private PerformanceMetricsCalculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new PerformanceMetricsCalculator();
        }

        [Test]
        public void CalculateROIGivenInitialInvestmentAndFinalValue_ShouldReturnCorrectROI()
        {
            decimal initialInvestment = 1000;
            decimal finalValue = 1500;

            decimal expectedROI = 50;

            decimal actualROI = calculator.CalculateROI(initialInvestment, finalValue);

            Assert.AreEqual(expectedROI, actualROI);
        }

        [Test]
        public void CalculateProfitFactorGivenTotalProfitsAndTotalLosses_ShouldReturnCorrectProfitFactor()
        {
            decimal totalProfits = 2000;
            decimal totalLosses = 1000;

            decimal expectedProfitFactor = 2;

            decimal actualProfitFactor = calculator.CalculateProfitFactor(totalProfits, totalLosses);

            Assert.AreEqual(expectedProfitFactor, actualProfitFactor);
        }

        [Test]
        public void CalculateRecoveryFactorGivenNetProfitsAndMaxDrawdown_ShouldReturnCorrectRecoveryFactor()
        {
            decimal netProfits = 3000;
            decimal maxDrawdown = 1500;

            decimal expectedRecoveryFactor = 2;

            decimal actualRecoveryFactor = calculator.CalculateRecoveryFactor(netProfits, maxDrawdown);

            Assert.AreEqual(expectedRecoveryFactor, actualRecoveryFactor);
        }

        [Test]
        public void CalculateExpectancyGivenWinRateAverageWinAndAverageLoss_ShouldReturnCorrectExpectancy()
        {
            decimal winRate = 0.6m;
            decimal averageWin = 500;
            decimal averageLoss = 300;

            decimal expectedExpectancy = 180;

            decimal actualExpectancy = calculator.CalculateExpectancy(winRate, averageWin, averageLoss);

            Assert.AreEqual(expectedExpectancy, actualExpectancy);
        }

        [Test]
        public void CalculateAverageTradeDurationGivenListOfTrades_ShouldReturnCorrectAverageTradeDuration()
        {
            List<Trade> trades = new List<Trade>()
        {
            new Trade { Duration = 5 },
            new Trade { Duration = 10 },
            new Trade { Duration = 15 }
        };

            decimal expectedAverageDuration = 10;
            var durations = trades.Select(x => x.Duration).ToList();
            decimal actualAverageDuration = calculator.CalculateAverageTradeDuration(durations);

            Assert.AreEqual(expectedAverageDuration, actualAverageDuration);
        }
    }

}