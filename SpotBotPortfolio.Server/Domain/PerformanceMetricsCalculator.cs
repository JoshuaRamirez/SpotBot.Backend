public class PerformanceMetricsCalculator
{
    public decimal CalculateROI(decimal initialInvestment, decimal finalValue)
    {
        return (finalValue - initialInvestment) / initialInvestment * 100;
    }

    public decimal CalculateProfitFactor(decimal totalProfits, decimal totalLosses)
    {
        if (totalLosses == 0)
        {
            return decimal.MaxValue; // Avoid division by zero
        }

        return totalProfits / totalLosses;
    }

    public decimal CalculateRecoveryFactor(decimal netProfits, decimal maxDrawdown)
    {
        if (maxDrawdown == 0)
        {
            return decimal.MaxValue; // Avoid division by zero
        }

        return netProfits / maxDrawdown;
    }

    public decimal CalculateExpectancy(decimal winRate, decimal averageWin, decimal averageLoss)
    {
        return (winRate * averageWin) - ((1 - winRate) * averageLoss);
    }

    public decimal CalculateAverageTradeDuration(List<decimal> tradeDurations)
    {
        if (tradeDurations.Count == 0)
        {
            return 0;
        }

        decimal totalDuration = tradeDurations.Sum();
        return totalDuration / tradeDurations.Count;
    }
}

public class Trade
{
    public decimal Duration { get; set; }
    // Add other properties relevant to a trade
}
