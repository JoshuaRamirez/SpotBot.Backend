using SpotBot.Server.Domain;

namespace SpotBot.Server.Tests.Unit;
[TestFixture]
public class PerformanceMetricsCalculatorTests
{
    [SetUp]
    public void Setup()
    {
        calculator = new PerformanceMetricsCalculator();
    }

    private PerformanceMetricsCalculator calculator;

    [Test]
    public void CalculateROIGivenInitialInvestmentAndFinalValue_ShouldReturnCorrectROI()
    {
        decimal initialInvestment = 1000;
        decimal finalValue = 1500;
        decimal expectedROI = 50;
        var actualROI = calculator.CalculateROI(initialInvestment, finalValue);
        Assert.That(expectedROI, Is.EqualTo(actualROI));
    }

    [Test]
    public void CalculateProfitFactorGivenTotalProfitsAndTotalLosses_ShouldReturnCorrectProfitFactor()
    {
        decimal totalProfits = 2000;
        decimal totalLosses = 1000;
        decimal expectedProfitFactor = 2;
        var actualProfitFactor = calculator.CalculateProfitFactor(totalProfits, totalLosses);
        Assert.That(expectedProfitFactor, Is.EqualTo(actualProfitFactor));
    }

    [Test]
    public void CalculateRecoveryFactorGivenNetProfitsAndMaxDrawdown_ShouldReturnCorrectRecoveryFactor()
    {
        decimal netProfits = 3000;
        decimal maxDrawdown = 1500;
        decimal expectedRecoveryFactor = 2;
        var actualRecoveryFactor = calculator.CalculateRecoveryFactor(netProfits, maxDrawdown);
        Assert.That(expectedRecoveryFactor, Is.EqualTo(actualRecoveryFactor));
    }

    [Test]
    public void CalculateExpectancyGivenWinRateAverageWinAndAverageLoss_ShouldReturnCorrectExpectancy()
    {
        var winRate = 0.6m;
        decimal averageWin = 500;
        decimal averageLoss = 300;
        decimal expectedExpectancy = 180;
        var actualExpectancy = calculator.CalculateExpectancy(winRate, averageWin, averageLoss);
        Assert.That(expectedExpectancy, Is.EqualTo(actualExpectancy));
    }

    [Test]
    public void CalculateAverageTradeDurationGivenListOfTrades_ShouldReturnCorrectAverageTradeDuration()
    {
        var trades = new List<Trade>
        {
            new Trade { Duration = 5 },
            new Trade { Duration = 10 },
            new Trade { Duration = 15 }
        };
        decimal expectedAverageDuration = 10;
        var durations = trades.Select(x => x.Duration).ToList();
        var actualAverageDuration = calculator.CalculateAverageTradeDuration(durations);
        Assert.That(expectedAverageDuration, Is.EqualTo(actualAverageDuration));
    }
}